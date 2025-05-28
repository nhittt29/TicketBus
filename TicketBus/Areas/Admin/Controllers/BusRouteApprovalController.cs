using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TicketBus.Data;
using TicketBus.Models;

namespace TicketBus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BusRouteApprovalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BusRouteApprovalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Admin/BusRouteApproval/PendingApproval
        public async Task<IActionResult> PendingApproval(string filter = "pending")
        {
            ViewBag.Filter = filter;

            IQueryable<BusRoute> query = _context.BusRoutes
                .AsNoTracking()
                .Include(r => r.Brand)
                .Include(r => r.StartCity)
                .Include(r => r.EndCity);

            switch (filter)
            {
                case "approved":
                    query = query.Where(r => r.State == BusRouteState.DaPheDuyet);
                    break;
                case "rejected":
                    query = query.Where(r => r.State == BusRouteState.TuChoi);
                    break;
                case "pending":
                default:
                    query = query.Where(r => r.State == BusRouteState.ChoPheDuyet);
                    break;
            }

            var routes = await query.OrderBy(r => r.IdRoute).ToListAsync();
            return View(routes);
        }

        // GET: /Admin/BusRouteApproval/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var route = await _context.BusRoutes
                .AsNoTracking()
                .Include(r => r.Brand)
                .Include(r => r.StartCity)
                .Include(r => r.EndCity)
                .Include(r => r.RouteStops)
                    .ThenInclude(rs => rs.City)
                .Include(r => r.Pickups)
                    .ThenInclude(p => p.City)
                .Include(r => r.DropOffs)
                    .ThenInclude(d => d.City)
                .FirstOrDefaultAsync(r => r.IdRoute == id);

            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // POST: /Admin/BusRouteApproval/Approve/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id, string reason)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "ID tuyến xe không hợp lệ." });
            }

            var route = await _context.BusRoutes
                .Include(r => r.Brand)
                .FirstOrDefaultAsync(r => r.IdRoute == id);

            if (route == null)
            {
                return Json(new { success = false, message = "Tuyến xe không tồn tại." });
            }

            if (route.State != BusRouteState.ChoPheDuyet)
            {
                return Json(new { success = false, message = "Tuyến xe không trong trạng thái chờ phê duyệt." });
            }

            route.State = BusRouteState.DaPheDuyet;
            _context.Update(route);

            var notification = new Notification
            {
                UserId = route.Brand?.UserId,
                Message = $"Tuyến xe '{route.NameRoute}' đã được phê duyệt bởi Admin." +
                          (string.IsNullOrEmpty(reason) ? "" : $" Lý do: {reason}"),
                CreatedDate = DateTime.Now,
                IsRead = false
            };
            _context.Notifications.Add(notification);

            try
            {
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Tuyến xe đã được phê duyệt thành công." });
            }
            catch (DbUpdateException ex)
            {
                return Json(new { success = false, message = "Lỗi khi cập nhật cơ sở dữ liệu: " + ex.Message });
            }
        }

        // POST: /Admin/BusRouteApproval/Reject/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id, string reason)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "ID tuyến xe không hợp lệ." });
            }

            var route = await _context.BusRoutes
                .Include(r => r.Brand)
                .FirstOrDefaultAsync(r => r.IdRoute == id);

            if (route == null)
            {
                return Json(new { success = false, message = "Tuyến xe không tồn tại." });
            }

            if (route.State != BusRouteState.ChoPheDuyet)
            {
                return Json(new { success = false, message = "Tuyến xe không trong trạng thái chờ phê duyệt." });
            }

            route.State = BusRouteState.TuChoi;
            _context.Update(route);

            var notification = new Notification
            {
                UserId = route.Brand?.UserId,
                Message = $"Tuyến xe '{route.NameRoute}' đã bị từ chối bởi Admin." +
                          (string.IsNullOrEmpty(reason) ? "" : $" Lý do: {reason}"),
                CreatedDate = DateTime.Now,
                IsRead = false
            };
            _context.Notifications.Add(notification);

            try
            {
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Tuyến xe đã bị từ chối thành công." });
            }
            catch (DbUpdateException ex)
            {
                return Json(new { success = false, message = "Lỗi khi cập nhật cơ sở dữ liệu: " + ex.Message });
            }
        }
    }
}