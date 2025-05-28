using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TicketBus.Data;
using TicketBus.Models;
using Microsoft.Extensions.Logging;

[Area("Passenger")]
[Authorize(Roles = "Passenger")]
public class TripController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<TripController> _logger;

    public TripController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<TripController> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    public IActionResult Index(string startPoint, string destination, DateTime? departureDate, string sortOption,
        string priceRange, string departureTime, int? operatorId, int? vehicleTypeId, int page = 1)
    {
        var cities = _context.Cities
            .Select(c => new { c.IdCity, c.NameCity })
            .OrderBy(c => c.NameCity)
            .ToList();

        var operators = _context.Brands
            .Select(b => new { b.IdBrand, b.NameBrand })
            .OrderBy(b => b.NameBrand)
            .ToList();

        var vehicleTypes = _context.VehicleTypes
            .Select(v => new { v.IdType, v.NameType })
            .OrderBy(v => v.NameType)
            .ToList();

        if (!departureDate.HasValue && !string.IsNullOrEmpty(Request.Query["departureDate"]))
        {
            if (DateTime.TryParse(Request.Query["departureDate"], CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                departureDate = parsedDate.Date;
            }
        }

        ViewBag.Cities = cities;
        ViewBag.Operators = operators;
        ViewBag.VehicleTypes = vehicleTypes;
        ViewBag.StartPoint = startPoint;
        ViewBag.Destination = destination;
        ViewBag.DepartureDate = departureDate?.ToString("yyyy-MM-dd");
        ViewBag.SortOption = sortOption;
        ViewBag.PriceRange = priceRange;
        ViewBag.DepartureTime = departureTime;
        ViewBag.OperatorId = operatorId;
        ViewBag.VehicleTypeId = vehicleTypeId;
        ViewBag.PageNumber = page;

        var pricesQuery = _context.Prices
            .Include(p => p.RouteStopStart)
            .Include(p => p.RouteStopEnd)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.BusRoute)
                    .ThenInclude(r => r.RouteStops)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.Coach)
                    .ThenInclude(c => c.Seats)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.Coach)
                    .ThenInclude(c => c.Brand)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.Coach)
                    .ThenInclude(c => c.VehicleType)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.BusRoute)
                    .ThenInclude(r => r.Pickups)
                        .ThenInclude(p => p.City)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.BusRoute)
                    .ThenInclude(r => r.DropOffs)
                        .ThenInclude(d => d.City)
            .AsSplitQuery()
            .AsQueryable();

        if (!string.IsNullOrEmpty(startPoint) && !string.IsNullOrEmpty(destination))
        {
            startPoint = startPoint.ToLower();
            destination = destination.ToLower();
            pricesQuery = pricesQuery.Where(p =>
                p.ScheduleDetails != null &&
                p.ScheduleDetails.BusRoute != null &&
                p.RouteStopStart != null &&
                p.RouteStopEnd != null &&
                p.RouteStopStart.StopName.ToLower().Contains(startPoint) &&
                p.RouteStopEnd.StopName.ToLower().Contains(destination));
        }

        if (!string.IsNullOrEmpty(priceRange))
        {
            var range = priceRange.Split('-');
            if (range.Length == 2 && decimal.TryParse(range[0], out var minPrice) && decimal.TryParse(range[1], out var maxPrice))
            {
                pricesQuery = pricesQuery.Where(p => p.PriceValue >= minPrice && p.PriceValue <= maxPrice);
            }
        }

        if (!string.IsNullOrEmpty(departureTime))
        {
            switch (departureTime)
            {
                case "morning":
                    pricesQuery = pricesQuery.Where(p => p.ScheduleDetails != null &&
                        p.ScheduleDetails.DepartTime != null &&
                        p.ScheduleDetails.DepartTime.Hours >= 6 &&
                        p.ScheduleDetails.DepartTime.Hours < 12);
                    break;
                case "afternoon":
                    pricesQuery = pricesQuery.Where(p => p.ScheduleDetails != null &&
                        p.ScheduleDetails.DepartTime != null &&
                        p.ScheduleDetails.DepartTime.Hours >= 12 &&
                        p.ScheduleDetails.DepartTime.Hours < 18);
                    break;
                case "evening":
                    pricesQuery = pricesQuery.Where(p => p.ScheduleDetails != null &&
                        p.ScheduleDetails.DepartTime != null &&
                        p.ScheduleDetails.DepartTime.Hours >= 18);
                    break;
            }
        }

        if (operatorId.HasValue)
        {
            pricesQuery = pricesQuery.Where(p => p.ScheduleDetails != null &&
                p.ScheduleDetails.Coach != null &&
                p.ScheduleDetails.Coach.Brand != null &&
                p.ScheduleDetails.Coach.IdBrand == operatorId.Value);
        }

        if (vehicleTypeId.HasValue)
        {
            pricesQuery = pricesQuery.Where(p => p.ScheduleDetails != null &&
                p.ScheduleDetails.Coach != null &&
                p.ScheduleDetails.Coach.VehicleType != null &&
                p.ScheduleDetails.Coach.IdType == vehicleTypeId.Value);
        }

        switch (sortOption)
        {
            case "earliest":
                pricesQuery = pricesQuery
                    .Where(p => p.RouteStopStart != null && p.RouteStopStart.Time != null)
                    .OrderBy(p => p.RouteStopStart.Time);
                break;
            case "price_asc":
                pricesQuery = pricesQuery.OrderBy(p => p.PriceValue);
                break;
            case "price_desc":
                pricesQuery = pricesQuery.OrderByDescending(p => p.PriceValue);
                break;
            default:
                pricesQuery = pricesQuery.OrderBy(p => p.IdPrice);
                break;
        }

        var prices = pricesQuery.ToList();
        var emptySeats = new Dictionary<int, int>();
        foreach (var price in prices)
        {
            if (price?.ScheduleDetails?.Coach?.Seats != null)
            {
                emptySeats[price.ScheduleDetails.IdCoach] = price.ScheduleDetails.Coach.Seats
                    .Count(s => s.State == SeatState.Trong);
            }
            else
            {
                emptySeats[price?.ScheduleDetails?.IdCoach ?? 0] = 0;
            }
        }
        ViewBag.EmptySeats = emptySeats;

        return View(prices);
    }

    public async Task<IActionResult> Details(int id, DateTime? departureDate)
    {
        _logger.LogInformation("[Details GET] id={Id}, departureDate={DepartureDate}", id, departureDate);

        var price = await _context.Prices
            .Include(p => p.RouteStopStart)
            .Include(p => p.RouteStopEnd)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.BusRoute)
                    .ThenInclude(r => r.RouteStops)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.Coach)
                    .ThenInclude(c => c.Seats)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.Coach)
                    .ThenInclude(c => c.Brand)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.Coach)
                    .ThenInclude(c => c.VehicleType)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.BusRoute)
                    .ThenInclude(r => r.Pickups)
                        .ThenInclude(p => p.City)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.BusRoute)
                    .ThenInclude(r => r.DropOffs)
                        .ThenInclude(d => d.City)
            .FirstOrDefaultAsync(p => p.IdPrice == id);

        if (price == null)
        {
            _logger.LogError("[Details GET] Không tìm thấy giá với idPrice={Id}", id);
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var passenger = await _context.Passengers
                .FirstOrDefaultAsync(p => p.UserId == user.Id);
            ViewBag.CustomerName = passenger?.NamePassenger ?? user.FullName;
            ViewBag.CustomerPhone = passenger?.PhoneNumber ?? user.PhoneNumber;
        }

        ViewBag.DepartureDate = departureDate?.Date ?? DateTime.Today;

        var SeatList = await _context.Seats
            .Include(s => s.Coach)
            .Where(s => s.Coach.IdCoach == price.ScheduleDetails.IdCoach)
            .ToListAsync();
        ViewBag.SeatList = SeatList;

        bool isSleeper = price.ScheduleDetails?.Coach?.VehicleType?.NameType.ToLower().Contains("giường nằm") == true;
        ViewBag.IsSleeper = isSleeper;

        if (isSleeper)
        {
            var totalSeats = SeatList.Count;
            var halfSeats = totalSeats / 2;
            ViewBag.UpperDeckSeats = SeatList.Take(halfSeats).ToList();
            ViewBag.LowerDeckSeats = SeatList.Skip(halfSeats).ToList();
        }

        var PickUpList = await _context.Pickups
            .Include(p => p.City)
            .Where(p => p.IdRoute == price.ScheduleDetails.BusRoute.IdRoute)
            .ToListAsync();
        ViewBag.PickUpList = PickUpList;

        var DropOffList = await _context.DropOffs
            .Include(d => d.City)
            .Where(d => d.IdRoute == price.ScheduleDetails.BusRoute.IdRoute)
            .ToListAsync();
        ViewBag.DropOffList = DropOffList;
        ViewBag.idPrice = price.IdPrice;

        var emptySeatsCount = SeatList.Count(s => s.State == SeatState.Trong);
        ViewBag.EmptySeats = new Dictionary<int, int>
        {
            { price.ScheduleDetails.IdCoach, emptySeatsCount }
        };

        return View(price);
    }

    [HttpPost]
    public async Task<IActionResult> Details(List<int> seatIds, int diemDi, int diemDen, int idPrice, DateTime? departureDate)
    {
        _logger.LogInformation("[Details POST] seatIds={SeatIds}, diemDi={DiemDi}, diemDen={DiemDen}, idPrice={IdPrice}, departureDate={DepartureDate}", seatIds, diemDi, diemDen, idPrice, departureDate);

        if (seatIds == null || seatIds.Count == 0)
        {
            _logger.LogWarning("[Details POST] Không có seatIds được chọn");
            ModelState.AddModelError(string.Empty, "Vui lòng chọn ít nhất một ghế!");
            return View(await GetPriceDetails(idPrice));
        }
        if (seatIds.Count > 4)
        {
            _logger.LogWarning("[Details POST] Chọn quá số lượng ghế cho phép: {Count}", seatIds.Count);
            ModelState.AddModelError(string.Empty, "Bạn chỉ được chọn tối đa 4 ghế!");
            return View(await GetPriceDetails(idPrice));
        }

        var pickupExists = await _context.Pickups.AnyAsync(p => p.IdPickup == diemDi);
        var dropOffExists = await _context.DropOffs.AnyAsync(d => d.IdDropOff == diemDen);
        if (!pickupExists || !dropOffExists)
        {
            _logger.LogWarning("[Details POST] Điểm đón (diemDi={DiemDi}) hoặc điểm trả (diemDen={DiemDen}) không hợp lệ", diemDi, diemDen);
            ModelState.AddModelError(string.Empty, "Điểm đón hoặc điểm trả không hợp lệ.");
            return View(await GetPriceDetails(idPrice));
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            _logger.LogError("[Details POST] Không tìm thấy thông tin người dùng");
            ModelState.AddModelError(string.Empty, "Không tìm thấy thông tin người dùng. Vui lòng đăng nhập lại.");
            return View(await GetPriceDetails(idPrice));
        }

        var passenger = await _context.Passengers.FirstOrDefaultAsync(p => p.UserId == user.Id);
        if (passenger == null)
        {
            _logger.LogError("[Details POST] Không tìm thấy thông tin hành khách cho userId={UserId}", user.Id);
            ModelState.AddModelError(string.Empty, "Không tìm thấy thông tin hành khách. Vui lòng liên hệ quản trị viên.");
            return View(await GetPriceDetails(idPrice));
        }

        var price = await _context.Prices
            .Include(p => p.RouteStopStart)
            .Include(p => p.RouteStopEnd)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.BusRoute)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.Coach)
                    .ThenInclude(c => c.Seats)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.Coach)
                    .ThenInclude(c => c.Brand)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.Coach)
                    .ThenInclude(c => c.VehicleType)
            .FirstOrDefaultAsync(p => p.IdPrice == idPrice);

        if (price == null)
        {
            _logger.LogError("[Details POST] Không tìm thấy thông tin giá với idPrice={IdPrice}", idPrice);
            return NotFound();
        }

        departureDate = departureDate?.Date ?? DateTime.Today;

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            foreach (var seatId in seatIds)
            {
                var seat = await _context.Seats.FirstOrDefaultAsync(s => s.IdSeat == seatId && s.State == SeatState.Trong);
                if (seat == null)
                {
                    _logger.LogWarning("[Details POST] Ghế với ID {SeatId} không khả dụng hoặc đã được đặt", seatId);
                    ModelState.AddModelError(string.Empty, $"Ghế với ID {seatId} không khả dụng hoặc đã được đặt.");
                    return View(await GetPriceDetails(idPrice));
                }

                var ticket = new Ticket
                {
                    IdPrice = idPrice,
                    IdSeat = seatId,
                    CreatedDate = DateTime.Now,
                    State = TicketState.ChuaThanhToan,
                    DepartureDate = departureDate,
                    TicketCode = GenerateTicketCode()
                };
                _logger.LogInformation("[Details POST] Tạo ticket: {@Ticket}", ticket);

                seat.State = SeatState.DaDat;
                _context.Update(seat);
                _context.Tickets.Add(ticket);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            _logger.LogInformation("[Details POST] Lưu vé thành công cho seatIds: {SeatIds}", seatIds);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "[Details POST] Lỗi khi lưu vé");
            ModelState.AddModelError(string.Empty, $"Lỗi khi lưu vé: {ex.Message}");
            return View(await GetPriceDetails(idPrice));
        }

        return RedirectToAction("Payment", new { idPrice, seatIds, pickupId = diemDi, dropOffId = diemDen, departureDate });
    }

    public async Task<IActionResult> Payment(int idPrice, List<int> seatIds, int pickupId, int dropOffId, DateTime? departureDate)
    {
        _logger.LogInformation("[Payment] idPrice={IdPrice}, seatIds={SeatIds}, pickupId={PickupId}, dropOffId={DropOffId}, departureDate={DepartureDate}", idPrice, seatIds, pickupId, dropOffId, departureDate);

        var price = await _context.Prices
            .Include(p => p.RouteStopStart)
            .Include(p => p.RouteStopEnd)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.BusRoute)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.Coach)
                    .ThenInclude(c => c.Seats)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.Coach)
                    .ThenInclude(c => c.Brand)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.Coach)
                    .ThenInclude(c => c.VehicleType)
            .FirstOrDefaultAsync(p => p.IdPrice == idPrice);

        if (price == null)
        {
            _logger.LogError("[Payment] Không tìm thấy thông tin giá với idPrice={IdPrice}", idPrice);
            return NotFound();
        }

        if (!departureDate.HasValue && !string.IsNullOrEmpty(Request.Query["departureDate"]))
        {
            if (DateTime.TryParse(Request.Query["departureDate"], CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                departureDate = parsedDate.Date;
            }
        }

        departureDate = departureDate?.Date ?? DateTime.Today;

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            _logger.LogError("[Payment] Không tìm thấy thông tin người dùng");
            return RedirectToAction("Login", "Account");
        }

        var passenger = await _context.Passengers
            .FirstOrDefaultAsync(p => p.UserId == user.Id);
        if (passenger == null)
        {
            _logger.LogError("[Payment] Không tìm thấy thông tin hành khách cho userId={UserId}", user.Id);
            return NotFound();
        }

        var seats = await _context.Seats
            .Where(s => seatIds.Contains(s.IdSeat))
            .Select(s => s.SeatCode)
            .ToListAsync();

        var pickup = await _context.Pickups
            .Include(p => p.City)
            .FirstOrDefaultAsync(p => p.IdPickup == pickupId);

        var dropOff = await _context.DropOffs
            .Include(d => d.City)
            .FirstOrDefaultAsync(d => d.IdDropOff == dropOffId);

        var tickets = await _context.Tickets
            .Include(t => t.Seat)
            .Where(t => t.IdPrice == idPrice && seatIds.Contains(t.IdSeat.Value) && t.DepartureDate == departureDate && t.State == TicketState.ChuaThanhToan)
            .OrderByDescending(t => t.CreatedDate)
            .ToListAsync();

        if (tickets == null || tickets.Count == 0)
        {
            _logger.LogError("[Payment] Không tìm thấy vé phù hợp với idPrice={IdPrice}, seatIds={SeatIds}", idPrice, seatIds);
            return NotFound();
        }

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var bill = new Bill
            {
                BillCode = "BILL-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                SeatQuantity = tickets.Count,
                Total = (long)(tickets.Count * price.PriceValue),
                FinalTotal = (decimal)(tickets.Count * price.PriceValue),
                PaymentStatus = "Pending",
                IdPassenger = passenger.IdPassenger,
                Tickets = tickets
            };
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            foreach (var ticket in tickets)
            {
                ticket.IdBill = bill.IdBill;
            }
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
            _logger.LogInformation("[Payment] Tạo bill và cập nhật vé thành công: BillCode={BillCode}, IdBill={IdBill}", bill.BillCode, bill.IdBill);

            ViewBag.Tickets = tickets;
            ViewBag.Bill = bill;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "[Payment] Lỗi khi tạo bill và cập nhật vé");
            ModelState.AddModelError(string.Empty, $"Lỗi khi xử lý: {ex.Message}");
            return View(price);
        }

        ViewBag.CustomerName = passenger?.NamePassenger ?? user.FullName;
        ViewBag.CustomerPhone = passenger?.PhoneNumber ?? user.PhoneNumber;
        ViewBag.SelectedSeats = seats;
        ViewBag.Pickup = pickup;
        ViewBag.DropOff = dropOff;
        ViewBag.DepartureDate = departureDate;

        return View(price);
    }

    public async Task<IActionResult> MyTickets()
    {
        _logger.LogInformation("[MyTickets] Retrieving tickets for user");

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            _logger.LogError("[MyTickets] Không tìm thấy thông tin người dùng");
            return RedirectToAction("Login", "Account");
        }

        var passenger = await _context.Passengers
            .FirstOrDefaultAsync(p => p.UserId == user.Id);
        if (passenger == null)
        {
            _logger.LogError("[MyTickets] Không tìm thấy thông tin hành khách cho userId={UserId}", user.Id);
            return NotFound();
        }

        var tickets = await _context.Tickets
            .Include(t => t.Seat)
            .Include(t => t.Price)
                .ThenInclude(p => p.RouteStopStart)
            .Include(t => t.Price)
                .ThenInclude(p => p.RouteStopEnd)
            .Include(t => t.Price)
                .ThenInclude(p => p.ScheduleDetails)
                    .ThenInclude(sd => sd.BusRoute)
            .Include(t => t.Price)
                .ThenInclude(p => p.ScheduleDetails)
                    .ThenInclude(sd => sd.Coach)
                        .ThenInclude(c => c.Brand)
            .Include(t => t.Price)
                .ThenInclude(p => p.ScheduleDetails)
                    .ThenInclude(sd => sd.Coach)
                        .ThenInclude(c => c.VehicleType)
            .Where(t => t.IdPrice != null && t.Price != null && t.Price.ScheduleDetails != null && t.Price.ScheduleDetails.BusRoute != null)
            .OrderByDescending(t => t.CreatedDate)
            .ToListAsync();

        return View(tickets);
    }
    private async Task<Price> GetPriceDetails(int idPrice)
    {
        return await _context.Prices
            .Include(p => p.RouteStopStart)
            .Include(p => p.RouteStopEnd)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.BusRoute)
                    .ThenInclude(r => r.RouteStops)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.Coach)
                    .ThenInclude(c => c.Seats)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.Coach)
                    .ThenInclude(c => c.Brand)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.Coach)
                    .ThenInclude(c => c.VehicleType)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.BusRoute)
                    .ThenInclude(r => r.Pickups)
                        .ThenInclude(p => p.City)
            .Include(p => p.ScheduleDetails)
                .ThenInclude(sd => sd.BusRoute)
                    .ThenInclude(r => r.DropOffs)
                        .ThenInclude(d => d.City)
            .FirstOrDefaultAsync(p => p.IdPrice == idPrice);
    }

    private string GenerateTicketCode()
    {
        return "TICKET-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
    }
}