using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketBus.Data;
using TicketBus.Models;
using TicketBus.Models.ViewModels;

namespace TicketBus.Areas.Brand.Controllers
{
    [Area("Brand")]
    [Authorize(Roles = "Brand")]
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScheduleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Brand/Schedule/Index
        public async Task<IActionResult> Index(int? coachFilter, int? routeFilter)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (brand == null)
            {
                return NotFound("Không tìm thấy hãng xe.");
            }

            var coaches = await _context.Coaches
                .AsNoTracking()
                .Where(c => c.IdBrand == brand.IdBrand && c.State == CoachState.DaPheDuyet)
                .Select(c => new { c.IdCoach, c.NumberPlate })
                .ToListAsync();

            var routes = await _context.BusRoutes
                .AsNoTracking()
                .Where(r => r.IdBrand == brand.IdBrand && r.State == BusRouteState.DaPheDuyet)
                .Select(r => new { r.IdRoute, r.NameRoute })
                .ToListAsync();

            ViewBag.Coaches = coaches.Select(c => new SelectListItem
            {
                Value = c.IdCoach.ToString(),
                Text = c.NumberPlate
            }).ToList();

            ViewBag.Routes = routes.Select(r => new SelectListItem
            {
                Value = r.IdRoute.ToString(),
                Text = r.NameRoute
            }).ToList();

            ViewBag.CoachFilter = coachFilter;
            ViewBag.RouteFilter = routeFilter;

            IQueryable<ScheduleDetails> query = _context.ScheduleDetails
                .AsNoTracking()
                .Include(s => s.Coach)
                .Include(s => s.BusRoute)
                .Where(s => s.BusRoute.IdBrand == brand.IdBrand);

            if (coachFilter.HasValue)
            {
                query = query.Where(s => s.IdCoach == coachFilter.Value);
            }

            if (routeFilter.HasValue)
            {
                query = query.Where(s => s.IdRoute == routeFilter.Value);
            }

            var schedules = await query.OrderBy(s => s.DepartTime).ToListAsync();
            return View(schedules);
        }

        // GET: /Brand/Schedule/Create
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (brand == null)
            {
                return NotFound("Không tìm thấy hãng xe.");
            }

            var routes = await _context.BusRoutes
                .AsNoTracking()
                .Where(r => r.IdBrand == brand.IdBrand && r.State == BusRouteState.DaPheDuyet)
                .Select(r => new SelectListItem
                {
                    Value = r.IdRoute.ToString(),
                    Text = r.NameRoute
                })
                .ToListAsync();

            var coaches = await _context.Coaches
                .AsNoTracking()
                .Where(c => c.IdBrand == brand.IdBrand && c.State == CoachState.DaPheDuyet)
                .Select(c => new SelectListItem
                {
                    Value = c.IdCoach.ToString(),
                    Text = c.NumberPlate
                })
                .ToListAsync();

            var model = new ScheduleDetailsViewModel
            {
                Routes = routes,
                Coaches = coaches,
                DepartHour = 7, // Giữ giá trị mặc định cho giờ để tiện nhập
                DepartMinute = 0,
                ArriveHour = 12,
                ArriveMinute = 0
            };
            return View(model);
        }

        // POST: /Brand/Schedule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScheduleDetailsViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (brand == null)
            {
                return NotFound("Không tìm thấy hãng xe.");
            }

            // Loại bỏ validation không cần thiết cho Routes và Coaches
            ModelState.Remove("Routes");
            ModelState.Remove("Coaches");

            // Kiểm tra giờ đến có lớn hơn giờ khởi hành không
            if (viewModel.ArriveTime <= viewModel.DepartTime)
            {
                ModelState.AddModelError("ArriveHour", "Giờ đến phải lớn hơn giờ khởi hành.");
            }

            // Kiểm tra binding dữ liệu
            if (viewModel.IdRoute == 0)
            {
                ModelState.AddModelError("IdRoute", "Tuyến xe là bắt buộc.");
            }

            if (viewModel.IdCoach == 0)
            {
                ModelState.AddModelError("IdCoach", "Xe là bắt buộc.");
            }

            if (ModelState.IsValid)
            {
                var schedule = new ScheduleDetails
                {
                    IdCoach = viewModel.IdCoach,
                    IdRoute = viewModel.IdRoute,
                    DepartTime = viewModel.DepartTime,
                    ArriveTime = viewModel.ArriveTime
                };

                var route = await _context.BusRoutes
                    .FirstOrDefaultAsync(r => r.IdRoute == schedule.IdRoute && r.IdBrand == brand.IdBrand && r.State == BusRouteState.DaPheDuyet);

                var coach = await _context.Coaches
                    .FirstOrDefaultAsync(c => c.IdCoach == schedule.IdCoach && c.IdBrand == brand.IdBrand && c.State == CoachState.DaPheDuyet);

                if (route == null)
                {
                    ModelState.AddModelError("IdRoute", "Tuyến xe không hợp lệ hoặc chưa được phê duyệt.");
                }

                if (coach == null)
                {
                    ModelState.AddModelError("IdCoach", "Xe không hợp lệ hoặc chưa được phê duyệt.");
                }

                if (!ModelState.IsValid)
                {
                    viewModel.Routes = await _context.BusRoutes
                        .AsNoTracking()
                        .Where(r => r.IdBrand == brand.IdBrand && r.State == BusRouteState.DaPheDuyet)
                        .Select(r => new SelectListItem
                        {
                            Value = r.IdRoute.ToString(),
                            Text = r.NameRoute
                        })
                        .ToListAsync();

                    viewModel.Coaches = await _context.Coaches
                        .AsNoTracking()
                        .Where(c => c.IdBrand == brand.IdBrand && c.State == CoachState.DaPheDuyet)
                        .Select(c => new SelectListItem
                        {
                            Value = c.IdCoach.ToString(),
                            Text = c.NumberPlate
                        })
                        .ToListAsync();

                    TempData["ErrorMessage"] = "Có lỗi xảy ra. Vui lòng kiểm tra lại thông tin.";
                    return View(viewModel);
                }

                try
                {
                    _context.Add(schedule);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Tạo lịch trình thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Lỗi khi lưu dữ liệu: {ex.Message}";
                }
            }
            else
            {
                // Hiển thị chi tiết lỗi validation
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["ErrorMessage"] = "Vui lòng điền đầy đủ và đúng thông tin. Chi tiết lỗi: " + string.Join("; ", errors);
            }

            viewModel.Routes = await _context.BusRoutes
                .AsNoTracking()
                .Where(r => r.IdBrand == brand.IdBrand && r.State == BusRouteState.DaPheDuyet)
                .Select(r => new SelectListItem
                {
                    Value = r.IdRoute.ToString(),
                    Text = r.NameRoute
                })
                .ToListAsync();

            viewModel.Coaches = await _context.Coaches
                .AsNoTracking()
                .Where(c => c.IdBrand == brand.IdBrand && c.State == CoachState.DaPheDuyet)
                .Select(c => new SelectListItem
                {
                    Value = c.IdCoach.ToString(),
                    Text = c.NumberPlate
                })
                .ToListAsync();

            return View(viewModel);
        }

        // GET: /Brand/Schedule/GetCoachesByRoute
        [HttpGet]
        public async Task<IActionResult> GetCoachesByRoute(int routeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (brand == null)
            {
                return Json(new { success = false, message = "Không tìm thấy hãng xe." });
            }

            var route = await _context.BusRoutes
                .FirstOrDefaultAsync(r => r.IdRoute == routeId && r.IdBrand == brand.IdBrand && r.State == BusRouteState.DaPheDuyet);

            if (route == null)
            {
                return Json(new { success = false, message = "Tuyến xe không hợp lệ hoặc chưa được phê duyệt." });
            }

            var coaches = await _context.Coaches
                .AsNoTracking()
                .Where(c => c.IdBrand == brand.IdBrand && c.State == CoachState.DaPheDuyet)
                .Select(c => new { c.IdCoach, c.NumberPlate })
                .ToListAsync();

            return Json(new { success = true, coaches });
        }

        // GET: /Brand/Schedule/AddPrice/5
        public async Task<IActionResult> AddPrice(int? id)
        {
            if (id == null)
            {
                return NotFound("Không tìm thấy lịch trình.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (brand == null)
            {
                return NotFound("Không tìm thấy hãng xe.");
            }

            var schedule = await _context.ScheduleDetails
                .Include(s => s.BusRoute)
                .Include(s => s.Coach)
                .FirstOrDefaultAsync(s => s.IdSchedule == id && s.BusRoute.IdBrand == brand.IdBrand);

            if (schedule == null)
            {
                return NotFound("Không tìm thấy lịch trình hoặc lịch trình không thuộc hãng xe của bạn.");
            }

            // Lấy danh sách RouteStop của BusRoute, mặc định là danh sách rỗng nếu không tìm thấy
            var routeStops = await _context.RouteStops
                .Where(rs => rs.IdRoute == schedule.IdRoute)
                .OrderBy(rs => rs.StopOrder)
                .ToListAsync() ?? new List<RouteStop>();

            if (routeStops.Count == 0)
            {
                return BadRequest("Tuyến xe không có điểm dừng nào được định nghĩa.");
            }

            // Lấy danh sách giá hiện có, mặc định là danh sách rỗng nếu không tìm thấy
            var existingPrices = await _context.Prices
                .Include(p => p.RouteStopStart)
                .Include(p => p.RouteStopEnd)
                .Where(p => p.IdSchedule == id)
                .ToListAsync() ?? new List<Price>();

            var viewModel = new AddPriceViewModel
            {
                ScheduleId = schedule.IdSchedule,
                ScheduleInfo = $"Lịch trình: {schedule.BusRoute.NameRoute} (Xe: {schedule.Coach.NumberPlate}, Khởi hành: {schedule.DepartTime})",
                RouteStops = routeStops,
                ExistingPrices = existingPrices
            };

            // Loại bỏ validation trên RouteStops và ExistingPrices
            ModelState.Remove("RouteStops");
            ModelState.Remove("ExistingPrices");

            return View(viewModel);
        }

        // POST: /Brand/Schedule/AddPrice
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPrice(AddPriceViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (brand == null)
            {
                return NotFound("Không tìm thấy hãng xe.");
            }

            // Lấy lại thông tin để hiển thị form nếu có lỗi
            var schedule = await _context.ScheduleDetails
                .Include(s => s.BusRoute)
                .Include(s => s.Coach)
                .FirstOrDefaultAsync(s => s.IdSchedule == model.ScheduleId && s.BusRoute.IdBrand == brand.IdBrand);

            if (schedule == null)
            {
                return NotFound("Không tìm thấy lịch trình hoặc lịch trình không thuộc hãng xe của bạn.");
            }

            var routeStops = await _context.RouteStops
                .Where(rs => rs.IdRoute == schedule.IdRoute)
                .OrderBy(rs => rs.StopOrder)
                .ToListAsync() ?? new List<RouteStop>();

            var existingPrices = await _context.Prices
                .Include(p => p.RouteStopStart)
                .Include(p => p.RouteStopEnd)
                .Where(p => p.IdSchedule == model.ScheduleId)
                .ToListAsync() ?? new List<Price>();

            model.ScheduleInfo = $"Lịch trình: {schedule.BusRoute.NameRoute} (Xe: {schedule.Coach.NumberPlate}, Khởi hành: {schedule.DepartTime})";
            model.RouteStops = routeStops;
            model.ExistingPrices = existingPrices;

            // Loại bỏ validation trên RouteStops và ExistingPrices
            ModelState.Remove("RouteStops");
            ModelState.Remove("ExistingPrices");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kiểm tra điểm bắt đầu và điểm kết thúc
            var startStop = routeStops.FirstOrDefault(rs => rs.IdStop == model.StartStopId);
            var endStop = routeStops.FirstOrDefault(rs => rs.IdStop == model.EndStopId);

            if (startStop == null || endStop == null)
            {
                ModelState.AddModelError("", "Điểm dừng không hợp lệ.");
                return View(model);
            }

            if (startStop.StopOrder >= endStop.StopOrder)
            {
                ModelState.AddModelError("", "Điểm bắt đầu phải trước điểm kết thúc trong tuyến xe.");
                return View(model);
            }

            // Kiểm tra xem giá đã tồn tại chưa
            var existingPrice = await _context.Prices
                .FirstOrDefaultAsync(p => p.IdSchedule == model.ScheduleId &&
                                         p.IdStopStart == model.StartStopId &&
                                         p.IdStopEnd == model.EndStopId);

            if (existingPrice != null)
            {
                ModelState.AddModelError("", "Giá vé cho đoạn đường này đã tồn tại.");
                return View(model);
            }

            // Tạo giá vé mới
            var price = new Price
            {
                IdSchedule = model.ScheduleId,
                PriceCode = $"{startStop.StopName}-{endStop.StopName}",
                PriceValue = model.PriceValue,
                IdRoute = schedule.IdRoute,
                IdStopStart = model.StartStopId,
                IdStopEnd = model.EndStopId,
                IdCoach = schedule.IdCoach
            };

            _context.Prices.Add(price);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Thêm giá vé thành công!";
            return RedirectToAction("AddPrice", new { id = model.ScheduleId }); // Quay lại form để thêm giá khác
        }
    }
}