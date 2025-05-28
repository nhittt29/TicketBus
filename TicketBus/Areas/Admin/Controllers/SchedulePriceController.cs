using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketBus.Data;
using TicketBus.Models;
using TicketBus.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SchedulePriceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SchedulePriceController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Admin/SchedulePrice/Index
        public async Task<IActionResult> Index()
        {
            var schedules = await _context.ScheduleDetails
                .Include(s => s.BusRoute)
                .Include(s => s.Coach)
                .Include(s => s.Prices)
                    .ThenInclude(p => p.RouteStopStart)
                .Include(s => s.Prices)
                    .ThenInclude(p => p.RouteStopEnd)
                .ToListAsync();

            return View(schedules);
        }

        // GET: /Admin/SchedulePrice/EditSchedule
        public async Task<IActionResult> EditSchedule(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.ScheduleDetails
                .FirstOrDefaultAsync(s => s.IdSchedule == id);
            if (schedule == null)
            {
                return NotFound();
            }

            var viewModel = new EditScheduleViewModel
            {
                IdSchedule = schedule.IdSchedule,
                IdCoach = schedule.IdCoach,
                IdRoute = schedule.IdRoute,
                DepartTime = schedule.DepartTime,
                ArriveTime = schedule.ArriveTime
            };

            ViewBag.BusRoutes = new SelectList(await _context.BusRoutes.ToListAsync(), "IdRoute", "NameRoute", schedule.IdRoute);
            ViewBag.Coaches = new SelectList(await _context.Coaches.ToListAsync(), "IdCoach", "NumberPlate", schedule.IdCoach);

            return View(viewModel);
        }

        // POST: /Admin/SchedulePrice/EditSchedule
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSchedule(int id, EditScheduleViewModel viewModel)
        {
            if (id != viewModel.IdSchedule)
            {
                return NotFound();
            }

            var schedule = await _context.ScheduleDetails
                .FirstOrDefaultAsync(s => s.IdSchedule == id);
            if (schedule == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Cập nhật chỉ các trường được cung cấp
                    if (viewModel.IdCoach.HasValue)
                        schedule.IdCoach = viewModel.IdCoach.Value;
                    if (viewModel.IdRoute.HasValue)
                        schedule.IdRoute = viewModel.IdRoute.Value;
                    if (viewModel.DepartTime.HasValue)
                        schedule.DepartTime = viewModel.DepartTime.Value;
                    if (viewModel.ArriveTime.HasValue)
                        schedule.ArriveTime = viewModel.ArriveTime.Value;

                    _context.Update(schedule);
                    await _context.SaveChangesAsync();

                    // Tạo thông báo cho người dùng Brand
                    var brandUsers = await _userManager.GetUsersInRoleAsync("Brand");
                    var notifications = brandUsers.Select(user => new Notification
                    {
                        UserId = user.Id,
                        Message = $"Lịch trình ID {schedule.IdSchedule} đã được cập nhật.",
                        CreatedDate = DateTime.Now,
                        IsRead = false
                    }).ToList();
                    _context.Notifications.AddRange(notifications);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Cập nhật lịch trình thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    TempData["ErrorMessage"] = "Lỗi khi cập nhật lịch trình. Vui lòng thử lại.";
                }
            }

            ViewBag.BusRoutes = new SelectList(await _context.BusRoutes.ToListAsync(), "IdRoute", "NameRoute", viewModel.IdRoute ?? schedule.IdRoute);
            ViewBag.Coaches = new SelectList(await _context.Coaches.ToListAsync(), "IdCoach", "NumberPlate", viewModel.IdCoach ?? schedule.IdCoach);
            return View(viewModel);
        }

        // GET: /Admin/SchedulePrice/AddPrice
        public async Task<IActionResult> AddPrice(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.ScheduleDetails
                .FirstOrDefaultAsync(s => s.IdSchedule == id);
            if (schedule == null)
            {
                return NotFound();
            }

            var routeStops = await _context.RouteStops
                .Where(rs => rs.IdRoute == schedule.IdRoute)
                .ToListAsync();

            ViewBag.RouteStops = new SelectList(routeStops, "IdStop", "StopName");
            ViewBag.ScheduleId = id;

            return View(new Price { IdSchedule = id.Value });
        }

        // POST: /Admin/SchedulePrice/AddPrice
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPrice(Price price)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Prices.Add(price);
                    await _context.SaveChangesAsync();

                    // Tạo thông báo cho người dùng Brand
                    var brandUsers = await _userManager.GetUsersInRoleAsync("Brand");
                    var notifications = brandUsers.Select(user => new Notification
                    {
                        UserId = user.Id,
                        Message = $"Giá vé mới đã được thêm cho lịch trình ID {price.IdSchedule}.",
                        CreatedDate = DateTime.Now,
                        IsRead = false
                    }).ToList();
                    _context.Notifications.AddRange(notifications);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Thêm giá vé thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    TempData["ErrorMessage"] = "Lỗi khi thêm giá vé. Vui lòng thử lại.";
                }
            }

            var schedule = await _context.ScheduleDetails
                .FirstOrDefaultAsync(s => s.IdSchedule == price.IdSchedule);
            var routeStops = await _context.RouteStops
                .Where(rs => rs.IdRoute == schedule.IdRoute)
                .ToListAsync();

            ViewBag.RouteStops = new SelectList(routeStops, "IdStop", "StopName");
            ViewBag.ScheduleId = price.IdSchedule;
            return View(price);
        }

        // GET: /Admin/SchedulePrice/DeleteSchedule
        public async Task<IActionResult> DeleteSchedule(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.ScheduleDetails
                .Include(s => s.BusRoute)
                .Include(s => s.Coach)
                .FirstOrDefaultAsync(s => s.IdSchedule == id);
            if (schedule == null)
            {
                return NotFound();
            }

            try
            {
                _context.ScheduleDetails.Remove(schedule);
                await _context.SaveChangesAsync();

                // Tạo thông báo cho người dùng Brand
                var brandUsers = await _userManager.GetUsersInRoleAsync("Brand");
                var notifications = brandUsers.Select(user => new Notification
                {
                    UserId = user.Id,
                    Message = $"Lịch trình ID {schedule.IdSchedule} đã bị xóa.",
                    CreatedDate = DateTime.Now,
                    IsRead = false
                }).ToList();
                _context.Notifications.AddRange(notifications);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Xóa lịch trình thành công.";
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "Lỗi khi xóa lịch trình. Vui lòng thử lại.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}