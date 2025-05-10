using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBus.Data;
using TicketBus.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TicketBus.Models.ViewModels;
using System.Linq;

namespace TicketBus.Areas.Brand.Controllers
{
    [Area("Brand")]
    [Authorize(Roles = "Brand")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Message"] = "Không thể xác định người dùng. Vui lòng đăng nhập lại.";
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var brand = await _context.Brands
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.UserId == userId && b.State == BrandState.HoatDong);

            if (brand == null)
            {
                TempData["Message"] = "Hãng xe của bạn chưa được phê duyệt hoặc không tồn tại.";
            }

            ViewBag.BrandInfo = brand;
            return View();
        }

        // GET: /Brand/Home/Chat
        public IActionResult Chat()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { error = "Không thể xác định người dùng." });
            }

            // Lấy role của người dùng hiện tại
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var isRestrictedRole = roles.Any(r => new[] { "Brand", "Passenger", "NhanVien" }.Contains(r));

            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            var users = isRestrictedRole
                ? admins.Select(u => new
                {
                    id = u.Id,
                    fullName = u.FullName,
                    isOnline = u.IsOnline
                }).ToList()
                : await _userManager.Users
                    .Where(u => u.Id != userId) // Loại trừ người dùng hiện tại
                    .Select(u => new
                    {
                        id = u.Id,
                        fullName = u.FullName,
                        isOnline = u.IsOnline
                    })
                    .ToListAsync();

            return Json(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetChatHistory(string receiverId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(receiverId))
            {
                _logger.LogWarning("GetChatHistory: Invalid user info - userId: {UserId}, receiverId: {ReceiverId}", userId, receiverId);
                return Json(new { error = "Thông tin người dùng không hợp lệ." });
            }

            var roomName = string.Join("-", new[] { userId, receiverId }.OrderBy(x => x));
            _logger.LogInformation("GetChatHistory: Fetching chat room with RoomName: {RoomName}, userId: {UserId}, receiverId: {ReceiverId}", roomName, userId, receiverId);

            var chatRoom = await _context.ChatRooms
                .Include(r => r.Messages)
                .FirstOrDefaultAsync(r => r.RoomName == roomName);

            if (chatRoom == null)
            {
                _logger.LogWarning("GetChatHistory: Chat room not found for RoomName: {RoomName}", roomName);
                return Json(new List<ChatMessageViewModel>());
            }

            // Không tự động cập nhật IsRead ở đây, chỉ lấy dữ liệu
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); // Windows
            // Nếu dùng Linux, thay bằng: TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh");

            var messages = chatRoom.Messages
                .OrderBy(m => m.SentDate)
                .Select(m => new ChatMessageViewModel
                {
                    senderId = m.SenderId,
                    senderName = _context.Users.FirstOrDefault(u => u.Id == m.SenderId)?.FullName ?? "Unknown",
                    content = m.Content,
                    sentDate = TimeZoneInfo.ConvertTimeFromUtc(m.SentDate, vietnamTimeZone), // Chuyển đổi sang múi giờ Việt Nam
                    isRead = m.IsRead
                }).ToList();

            _logger.LogInformation("GetChatHistory: Loaded {Count} messages for RoomName: {RoomName}", messages.Count, roomName);
            foreach (var msg in messages)
            {
                _logger.LogInformation("GetChatHistory: Message - SenderId: {SenderId}, Content: {Content}, SentDate: {SentDate}, IsRead: {IsRead}", msg.senderId, msg.content, msg.sentDate, msg.isRead);
            }

            return Json(messages);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(string receiverId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(receiverId))
            {
                _logger.LogWarning("MarkAsRead: Invalid user info - userId: {UserId}, receiverId: {ReceiverId}", userId, receiverId);
                return Json(new { error = "Thông tin người dùng không hợp lệ." });
            }

            var roomName = string.Join("-", new[] { userId, receiverId }.OrderBy(x => x));
            var chatRoom = await _context.ChatRooms
                .Include(r => r.Messages)
                .FirstOrDefaultAsync(r => r.RoomName == roomName);

            if (chatRoom == null)
            {
                _logger.LogWarning("MarkAsRead: Chat room not found for RoomName: {RoomName}", roomName);
                return Json(new { success = false });
            }

            var unreadMessages = chatRoom.Messages
                .Where(m => m.ReceiverId == userId && !m.IsRead)
                .ToList();

            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.IsRead = true;
                    _logger.LogInformation("MarkAsRead: Marking message as read - MessageId: {MessageId}, Content: {Content}", message.Id, message.Content);
                }
                await _context.SaveChangesAsync();
                _logger.LogInformation("MarkAsRead: Updated {Count} messages as read for RoomName: {RoomName}", unreadMessages.Count, roomName);
            }

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetApprovedCoaches()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { coaches = new List<object>() });
            }

            var brand = await _context.Brands
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.UserId == userId && b.State == BrandState.HoatDong);

            if (brand == null)
            {
                return Json(new { coaches = new List<object>() });
            }

            var coaches = await _context.Coaches
                .Where(c => c.IdBrand == brand.IdBrand && c.State == CoachState.DaPheDuyet)
                .Include(c => c.VehicleType)
                .Select(c => new
                {
                    coachCode = c.CoachCode,
                    numberPlate = c.NumberPlate,
                    vehicleType = c.VehicleType != null ? c.VehicleType.NameType : null,
                    state = (int)c.State
                })
                .ToListAsync();

            return Json(new { coaches });
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { unreadCount = 0, notifications = new List<object>() });
            }

            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedDate)
                .Take(10)
                .Select(n => new
                {
                    id = n.Id,
                    message = n.Message,
                    createdDate = n.CreatedDate.ToString("o"),
                    isRead = n.IsRead
                })
                .ToListAsync();

            var unreadCount = notifications.Count(n => !n.isRead);

            return Json(new { unreadCount, notifications });
        }

        public IActionResult GoToHomePage()
        {
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkNotificationAsRead(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("MarkNotificationAsRead: UserId is null or empty.");
                return Json(new { success = false, message = "Không thể xác định người dùng." });
            }

            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);
            if (notification == null)
            {
                _logger.LogWarning("MarkNotificationAsRead: Notification with ID {Id} not found for UserId {UserId}.", id, userId);
                return Json(new { success = false, message = "Thông báo không tồn tại." });
            }

            try
            {
                notification.IsRead = true;
                _context.Update(notification);
                await _context.SaveChangesAsync();
                _logger.LogInformation("MarkNotificationAsRead: Notification {Id} marked as read for UserId {UserId}.", id, userId);
                return Json(new { success = true, message = "Đã đánh dấu thông báo là đã đọc." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MarkNotificationAsRead: Failed to mark notification {Id} as read for UserId {UserId}.", id, userId);
                return Json(new { success = false, message = "Có lỗi xảy ra khi đánh dấu thông báo. Vui lòng thử lại." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAllNotificationsAsRead()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("MarkAllNotificationsAsRead: UserId is null or empty.");
                return Json(new { success = false, message = "Không thể xác định người dùng." });
            }

            try
            {
                var unreadNotifications = await _context.Notifications
                    .Where(n => n.UserId == userId && !n.IsRead)
                    .ToListAsync();

                if (!unreadNotifications.Any())
                {
                    return Json(new { success = true, message = "Không có thông báo chưa đọc." });
                }

                foreach (var notification in unreadNotifications)
                {
                    notification.IsRead = true;
                }

                _context.UpdateRange(unreadNotifications);
                await _context.SaveChangesAsync();
                _logger.LogInformation("MarkAllNotificationsAsRead: All notifications marked as read for UserId {UserId}.", userId);
                return Json(new { success = true, message = "Đã đánh dấu tất cả thông báo là đã đọc." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MarkAllNotificationsAsRead: Failed to mark all notifications as read for UserId {UserId}.", userId);
                return Json(new { success = false, message = "Có lỗi xảy ra khi đánh dấu thông báo. Vui lòng thử lại." });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUnreadMessageCount()
        {
            _logger.LogInformation("GetUnreadMessageCount: API called.");
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("GetUnreadMessageCount: UserId is null or empty.");
                return Json(new { unreadCount = 0 });
            }

            var unreadCount = await _context.ChatMessages
                .Where(m => m.ReceiverId == userId && !m.IsRead)
                .CountAsync();

            _logger.LogInformation("GetUnreadMessageCount: Total unread messages for UserId {UserId} is {UnreadCount}", userId, unreadCount);
            return Json(new { unreadCount });
        }
    }
}