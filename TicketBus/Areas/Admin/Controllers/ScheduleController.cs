using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBus.Data;
using TicketBus.Models;

namespace TicketBus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScheduleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Admin/Schedule/Index
        public async Task<IActionResult> Index()
        {
            var schedules = await _context.ScheduleDetails
                .AsNoTracking()
                .Include(s => s.Coach)
                .Include(s => s.BusRoute)
                .ThenInclude(r => r.Brand)
                .OrderBy(s => s.DepartTime)
                .ToListAsync();

            return View(schedules);
        }

        // GET: /Admin/Schedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Không tìm thấy lịch trình.");
            }

            var schedule = await _context.ScheduleDetails
                .AsNoTracking()
                .Include(s => s.Coach)
                .Include(s => s.BusRoute)
                .ThenInclude(r => r.Brand)
                .FirstOrDefaultAsync(s => s.IdSchedule == id);

            if (schedule == null)
            {
                return NotFound("Không tìm thấy lịch trình.");
            }

            return View(schedule);
        }

        // GET: /Admin/Schedule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound("Không tìm thấy lịch trình.");
            }

            var schedule = await _context.ScheduleDetails
                .AsNoTracking()
                .Include(s => s.Coach)
                .Include(s => s.BusRoute)
                .ThenInclude(r => r.Brand)
                .FirstOrDefaultAsync(s => s.IdSchedule == id);

            if (schedule == null)
            {
                return NotFound("Không tìm thấy lịch trình.");
            }

            return View(schedule);
        }

        // POST: /Admin/Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string reason)
        {
            // Tìm lịch trình theo IdSchedule
            var schedule = await _context.ScheduleDetails
                .FirstOrDefaultAsync(s => s.IdSchedule == id);

            if (schedule == null)
            {
                TempData["ErrorMessage"] = "Lịch trình không tồn tại.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                // Tìm thông tin Brand để gửi thông báo
                var scheduleWithDetails = await _context.ScheduleDetails
                    .AsNoTracking()
                    .Include(s => s.BusRoute)
                    .ThenInclude(r => r.Brand)
                    .FirstOrDefaultAsync(s => s.IdSchedule == id);

                string brandUserId = null;
                string notificationMessage = "Lịch trình của bạn đã bị xóa bởi Admin.";

                if (scheduleWithDetails != null && scheduleWithDetails.BusRoute != null && scheduleWithDetails.BusRoute.Brand != null)
                {
                    brandUserId = scheduleWithDetails.BusRoute.Brand.UserId;
                    notificationMessage = $"Lịch trình tuyến '{scheduleWithDetails.BusRoute.NameRoute}' (xe: {scheduleWithDetails.Coach?.NumberPlate ?? "N/A"}, giờ khởi hành: {scheduleWithDetails.DepartTime.ToString(@"hh\:mm")}) đã bị xóa bởi Admin. Lý do: {reason ?? "Không có lý do được cung cấp."}";
                }

                // Xóa lịch trình
                _context.ScheduleDetails.Remove(schedule);
                await _context.SaveChangesAsync();

                // Gửi thông báo nếu tìm thấy Brand
                if (!string.IsNullOrEmpty(brandUserId))
                {
                    var notification = new Notification
                    {
                        UserId = brandUserId,
                        Message = notificationMessage,
                        CreatedDate = DateTime.Now,
                        IsRead = false
                    };
                    _context.Notifications.Add(notification);
                    await _context.SaveChangesAsync();
                }

                TempData["Message"] = "Xóa lịch trình thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi xóa lịch trình: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}