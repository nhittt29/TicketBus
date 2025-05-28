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
using Microsoft.AspNetCore.SignalR;
using TicketBus.Hubs;

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
        private readonly IHubContext<ChatHub> _hubContext;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _hubContext = hubContext;
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

        // GET: /Brand/Home/TicketList
        public async Task<IActionResult> TicketList()
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
                return View(new List<object>());
            }

            var tickets = await _context.Tickets
                .AsNoTracking()
                .Include(t => t.Price)
                    .ThenInclude(p => p.ScheduleDetails)
                    .ThenInclude(sd => sd.BusRoute)
                .Include(t => t.Price)
                    .ThenInclude(p => p.RouteStopStart)
                .Include(t => t.Price)
                    .ThenInclude(p => p.RouteStopEnd)
                .Include(t => t.Seat)
                .Include(t => t.Bill)
                    .ThenInclude(b => b.Passenger)
                .Where(t => t.Price.ScheduleDetails.BusRoute.IdBrand == brand.IdBrand)
                .Select(t => new
                {
                    TicketId = t.IdTicket,
                    TicketCode = t.TicketCode,
                    PassengerName = t.Bill.Passenger.NamePassenger,
                    PassengerPhone = t.Bill.Passenger.PhoneNumber,
                    RouteName = t.Price.ScheduleDetails.BusRoute.NameRoute,
                    StartStop = t.Price.RouteStopStart.StopName,
                    EndStop = t.Price.RouteStopEnd.StopName,
                    DepartureDate = t.DepartureDate,
                    SeatCode = t.Seat.SeatCode,
                    PriceValue = t.Price.PriceValue,
                    PaymentStatus = t.Bill.PaymentStatus,
                    TicketState = t.State
                })
                .OrderByDescending(t => t.DepartureDate)
                .ToListAsync();

            return View(tickets);
        }

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
                    .Where(u => u.Id != userId)
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

            // Chỉ lấy ChatRoom với IsDeleted = false
            var chatRoom = await _context.ChatRooms
                .Include(r => r.Messages)
                .FirstOrDefaultAsync(r => r.RoomName == roomName && !r.IsDeleted);

            if (chatRoom == null)
            {
                _logger.LogWarning("GetChatHistory: Chat room not found or deleted for RoomName: {RoomName}", roomName);
                return Json(new List<ChatMessageViewModel>());
            }

            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            var messages = chatRoom.Messages
                .OrderBy(m => m.SentDate)
                .Select(m => new ChatMessageViewModel
                {
                    senderId = m.SenderId,
                    senderName = _context.Users.FirstOrDefault(u => u.Id == m.SenderId)?.FullName ?? "Unknown",
                    content = m.Content,
                    sentDate = TimeZoneInfo.ConvertTimeFromUtc(m.SentDate, vietnamTimeZone),
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
                .FirstOrDefaultAsync(r => r.RoomName == roomName && !r.IsDeleted);

            if (chatRoom == null)
            {
                _logger.LogWarning("MarkAsRead: Chat room not found or deleted for RoomName: {RoomName}", roomName);
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
        public async Task<IActionResult> GetAcceptedAdmins()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("GetAcceptedAdmins: UserId is null or empty.");
                return Json(new { error = "Không thể xác định người dùng." });
            }

            _logger.LogInformation("GetAcceptedAdmins: Fetching chat requests for UserId: {UserId}", userId);

            var chatRequests = await _context.ChatRequests
                .Where(r => r.SenderId == userId && r.IsAccepted)
                .ToListAsync();

            _logger.LogInformation("GetAcceptedAdmins: Found {Count} accepted chat requests for UserId: {UserId}", chatRequests.Count, userId);
            foreach (var request in chatRequests)
            {
                _logger.LogInformation("GetAcceptedAdmins: ChatRequest - SenderId: {SenderId}, ReceiverId: {ReceiverId}, IsAccepted: {IsAccepted}", request.SenderId, request.ReceiverId, request.IsAccepted);
            }

            var acceptedRequests = chatRequests
                .Select(r =>
                {
                    var admin = _context.Users.FirstOrDefault(u => u.Id == r.ReceiverId);
                    var roomName = string.Join("-", new[] { userId, r.ReceiverId }.OrderBy(x => x));
                    var chatRoom = _context.ChatRooms
                        .Where(cr => cr.RoomName == roomName && !cr.IsDeleted)
                        .OrderByDescending(cr => cr.CreatedDate)
                        .FirstOrDefault();
                    _logger.LogInformation("GetAcceptedAdmins: Checking ChatRoom for RoomName: {RoomName}, Exists: {Exists}", roomName, chatRoom != null);

                    // Đếm số tin nhắn chưa đọc trong đoạn chat này
                    var unreadCount = chatRoom != null
                        ? _context.ChatMessages
                            .Where(m => m.Id == chatRoom.Id && m.ReceiverId == userId && !m.IsRead)
                            .Count()
                        : 0;

                    return new
                    {
                        adminId = r.ReceiverId,
                        adminName = admin != null ? (admin.FullName ?? admin.UserName ?? "Admin") : "Admin",
                        hasChatRoom = chatRoom != null,
                        unreadCount = unreadCount // Thêm số tin nhắn chưa đọc
                    };
                })
                .Where(r => r.hasChatRoom)
                .Select(r => new { r.adminId, r.adminName, r.unreadCount })
                .ToList();

            _logger.LogInformation("GetAcceptedAdmins: Loaded {Count} accepted admins for UserId: {UserId}.", acceptedRequests.Count, userId);
            return Json(acceptedRequests);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteChat(string receiverId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(receiverId))
            {
                _logger.LogWarning("DeleteChat: Invalid user info - userId: {UserId}, receiverId: {ReceiverId}.", userId, receiverId);
                return Json(new { success = false, message = "Thông tin người dùng không hợp lệ." });
            }

            try
            {
                var roomName = string.Join("-", new[] { userId, receiverId }.OrderBy(x => x));
                var chatRoom = await _context.ChatRooms
                    .FirstOrDefaultAsync(r => r.RoomName == roomName && !r.IsDeleted);

                if (chatRoom == null)
                {
                    _logger.LogWarning("DeleteChat: Chat room not found or already deleted for RoomName: {RoomName}.", roomName);
                    return Json(new { success = false, message = "Đoạn chat không tồn tại." });
                }

                chatRoom.IsDeleted = true;

                var chatRequest = await _context.ChatRequests
                    .FirstOrDefaultAsync(r => (r.SenderId == userId && r.ReceiverId == receiverId) || (r.SenderId == receiverId && r.ReceiverId == userId));
                if (chatRequest != null)
                {
                    _context.ChatRequests.Remove(chatRequest);
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("DeleteChat: Chat marked as deleted for RoomName: {RoomName}.", roomName);

                await _hubContext.Clients.User(userId).SendAsync("ChatDeleted", receiverId);
                await _hubContext.Clients.User(receiverId).SendAsync("ChatDeleted", userId);

                return Json(new { success = true, message = "Đã xóa đoạn chat thành công." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteChat: Error occurred while deleting chat for RoomName: {RoomName}.", string.Join("-", new[] { userId, receiverId }.OrderBy(x => x)));
                return Json(new { success = false, message = "Có lỗi xảy ra khi xóa đoạn chat. Vui lòng thử lại." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetApprovedCoaches()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("GetApprovedCoaches: UserId is null or empty.");
                return Json(new { coaches = new List<object>() });
            }

            var brand = await _context.Brands
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (brand == null)
            {
                _logger.LogWarning("GetApprovedCoaches: Brand not found for UserId: {UserId}.", userId);
                return Json(new { coaches = new List<object>() });
            }

            var coaches = await _context.Coaches
                .AsNoTracking()
                .Where(c => c.IdBrand == brand.IdBrand && c.State == CoachState.DaPheDuyet)
                .Include(c => c.VehicleType)
                .Select(c => new
                {
                    coachCode = c.CoachCode,
                    numberPlate = c.NumberPlate,
                    vehicleType = c.VehicleType != null ? c.VehicleType.NameType : "N/A",
                    state = c.State
                })
                .ToListAsync();

            _logger.LogInformation("GetApprovedCoaches: Loaded {Count} approved coaches for UserId {UserId}.", coaches.Count, userId);
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

        [HttpGet]
        [Authorize(Roles = "Brand")]
        public async Task<IActionResult> GetUserName(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { error = "UserId không hợp lệ." });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Json(new { error = "Người dùng không tồn tại." });
            }

            return Json(new { fullName = user.FullName ?? user.UserName ?? "Brand" });
        }
    }
}