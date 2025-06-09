using Microsoft.AspNetCore.Mvc;
using TicketBus.Data;
using TicketBus.Models;
using TicketBus.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TicketBus.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Search(string origin, string destination, DateTime date)
        {
            if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination) || date == default)
            {
                return RedirectToAction("Index", "Home");
            }

            var targetDate = date.Date;
            var scheduleDetailsList = _context.ScheduleDetails
                .Include(sd => sd.Coach)
                .ThenInclude(c => c.VehicleType)
                .Include(sd => sd.Coach.Brand)
                .Include(sd => sd.BusRoute)
                .ThenInclude(br => br.StartCity)
                .Include(sd => sd.BusRoute)
                .ThenInclude(br => br.EndCity)
                .Include(sd => sd.Prices) // Đảm bảo tải Prices
                .Where(sd => sd.BusRoute.StartCity.CityCode == origin &&
                            sd.BusRoute.EndCity.CityCode == destination &&
                            sd.DepartTime >= TimeSpan.Zero)
                .ToList();

            var availableTrips = scheduleDetailsList
                .Select(sd => new
                {
                    ScheduleDetails = sd,
                    OccupiedSeats = sd.Prices.Any() ? _context.Tickets
                        .Where(t => t.IdPrice == sd.Prices.First().IdPrice &&
                                   t.DepartureDate.HasValue &&
                                   t.DepartureDate.Value.Date == targetDate &&
                                   (t.State == TicketState.DaThanhToan || (t.State == TicketState.ChuaThanhToan && t.PaymentMethod == "AtBus")))
                        .Count() : 0
                })
                .Select(x => new AvailableTripViewModel
                {
                    RouteName = x.ScheduleDetails.BusRoute.NameRoute,
                    StartCity = x.ScheduleDetails.BusRoute.StartCity.NameCity,
                    EndCity = x.ScheduleDetails.BusRoute.EndCity.NameCity,
                    DepartureTime = targetDate + x.ScheduleDetails.DepartTime, // Sử dụng targetDate thay vì DateTime.Today
                    BrandName = x.ScheduleDetails.Coach.Brand.NameBrand,
                    VehicleType = x.ScheduleDetails.Coach.VehicleType.NameType,
                    TotalSeats = x.ScheduleDetails.Coach.VehicleType.SeatCount,
                    AvailableSeats = x.ScheduleDetails.Coach.VehicleType.SeatCount - x.OccupiedSeats
                })
                .Where(t => t.AvailableSeats > 0)
                .ToList();

            if (availableTrips == null || !availableTrips.Any())
            {
                return View("NotFound");
            }

            return View(availableTrips);
        }
    }
}