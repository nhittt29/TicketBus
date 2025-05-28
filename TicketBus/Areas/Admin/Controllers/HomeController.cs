using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBus.Data;
using TicketBus.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketBus.Models.ViewModels;
using Microsoft.AspNetCore.SignalR;
using TicketBus.Hubs;

namespace TicketBus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHubContext<ChatHub> _hubContext;

        public HomeController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _hubContext = hubContext;
        }

        // GET: /Admin/Home/AdminPanel
        public async Task<IActionResult> AdminPanel()
        {
            // Lấy số lượng hãng xe đang chờ phê duyệt
            var pendingBrandsCount = await _context.Brands
                .CountAsync(b => b.State == BrandState.ChoPheDuyet);

            // Lấy số lượng tuyến xe đang chờ phê duyệt
            var pendingBusRoutesCount = await _context.BusRoutes
                .CountAsync(r => r.State == BusRouteState.ChoPheDuyet);

            // Lấy số lượng xe đang chờ phê duyệt
            var pendingCoachesCount = await _context.Coaches
                .CountAsync(c => c.State == CoachState.ChoPheDuyet);

            // Lấy danh sách thông báo mới nhất (giới hạn 5 thông báo)
            var notifications = await _context.Notifications
                .OrderByDescending(n => n.CreatedDate)
                .Take(5)
                .ToListAsync();

            // **Dữ liệu cho Area Chart 1: Số tuyến xe theo trạng thái**
            var busRoutesByState = await _context.BusRoutes
                .GroupBy(r => r.State)
                .Select(g => new
                {
                    State = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var labelsAreaChart1 = new List<string> { "Chờ phê duyệt", "Đã phê duyệt", "Từ chối" };
            var dataAreaChart1 = new List<int>
            {
                busRoutesByState.FirstOrDefault(s => s.State == BusRouteState.ChoPheDuyet)?.Count ?? 0,
                busRoutesByState.FirstOrDefault(s => s.State == BusRouteState.DaPheDuyet)?.Count ?? 0,
                busRoutesByState.FirstOrDefault(s => s.State == BusRouteState.TuChoi)?.Count ?? 0
            };

            // **Dữ liệu cho Area Chart 2: Số thông báo theo tháng (6 tháng gần nhất)**
            var sixMonthsAgo = DateTime.UtcNow.AddMonths(-6);
            var notificationsByMonth = await _context.Notifications
                .Where(n => n.CreatedDate >= sixMonthsAgo)
                .GroupBy(n => new { n.CreatedDate.Year, n.CreatedDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(g => g.Year).ThenBy(g => g.Month)
                .ToListAsync();

            var labelsAreaChart2 = new List<string>();
            var dataAreaChart2 = new List<int>();
            var currentDate = sixMonthsAgo;
            while (currentDate <= DateTime.UtcNow)
            {
                labelsAreaChart2.Add(currentDate.ToString("MM/yyyy"));
                var matchingMonth = notificationsByMonth
                    .FirstOrDefault(m => m.Year == currentDate.Year && m.Month == currentDate.Month);
                dataAreaChart2.Add(matchingMonth?.Count ?? 0);
                currentDate = currentDate.AddMonths(1);
            }

            // **Dữ liệu cho Bar Chart: Số xe theo hãng xe**
            var coachesByBrand = await _context.Coaches
                .Include(c => c.Brand)
                .GroupBy(c => c.Brand)
                .Select(g => new
                {
                    BrandName = g.Key != null ? g.Key.NameBrand : "Không có hãng",
                    Count = g.Count()
                })
                .OrderBy(g => g.BrandName)
                .ToListAsync();

            var labelsBarChart = coachesByBrand.Select(b => b.BrandName).ToList();
            var dataBarChart = coachesByBrand.Select(b => b.Count).ToList();

            // **Dữ liệu cho Pie Chart: Tỷ lệ thông báo đã đọc/chưa đọc**
            var notificationsByReadStatus = await _context.Notifications
                .GroupBy(n => n.IsRead)
                .Select(g => new
                {
                    IsRead = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var labelsPieChart = new List<string> { "Đã đọc", "Chưa đọc" };
            var dataPieChart = new List<int>
            {
                notificationsByReadStatus.FirstOrDefault(n => n.IsRead)?.Count ?? 0,
                notificationsByReadStatus.FirstOrDefault(n => !n.IsRead)?.Count ?? 0
            };

            // **Dữ liệu cho Scatter Chart: Giá vé trung bình theo khoảng cách**
            var priceByDistance = await _context.Prices
                .Include(p => p.ScheduleDetails)
                    .ThenInclude(s => s.BusRoute)
                .GroupBy(p => p.ScheduleDetails.IdRoute)
                .Select(g => new
                {
                    RouteDistance = g.First().ScheduleDetails.BusRoute.Distance ?? 0,
                    AvgPrice = g.Average(p => (double)p.PriceValue)
                })
                .Where(g => g.RouteDistance > 0)
                .ToListAsync();

            var dataScatterChart = priceByDistance.Select(g => new Dictionary<string, object>
            {
                { "x", g.RouteDistance },
                { "y", g.AvgPrice }
            }).ToList();

            // Truyền dữ liệu vào ViewBag
            ViewBag.PendingBrandsCount = pendingBrandsCount;
            ViewBag.PendingBusRoutesCount = pendingBusRoutesCount;
            ViewBag.PendingCoachesCount = pendingCoachesCount;
            ViewBag.Notifications = notifications;

            // Dữ liệu cho biểu đồ
            ViewBag.LabelsAreaChart1 = labelsAreaChart1;
            ViewBag.DataAreaChart1 = dataAreaChart1;
            ViewBag.LabelsAreaChart2 = labelsAreaChart2;
            ViewBag.DataAreaChart2 = dataAreaChart2;
            ViewBag.LabelsBarChart = labelsBarChart;
            ViewBag.DataBarChart = dataBarChart;
            ViewBag.LabelsPieChart = labelsPieChart;
            ViewBag.DataPieChart = dataPieChart;
            ViewBag.DataScatterChart = dataScatterChart;

            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetChatHistory(string receiverId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(receiverId))
            {
                return Json(new { error = "Thông tin người dùng không hợp lệ." });
            }

            var roomName = string.Join("-", new[] { userId, receiverId }.OrderBy(x => x));
            var chatRoom = await _context.ChatRooms
                .Include(r => r.Messages)
                .FirstOrDefaultAsync(r => r.RoomName == roomName && !r.IsDeleted);

            if (chatRoom == null)
            {
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

            return Json(messages);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(string receiverId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(receiverId))
            {
                return Json(new { error = "Thông tin người dùng không hợp lệ." });
            }

            // Phương thức này giờ sẽ được thay thế bằng SignalR Hub (MarkAsRead trong ChatHub.cs)
            // Vì vậy, không cần logic này nữa, client sẽ gọi trực tiếp Hub
            return Json(new { success = true });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetChatRequests()
        {
            var requests = await _context.ChatRequests
                .Where(r => r.ReceiverRole == "Admin" && !r.IsAccepted)
                .Select(r => new
                {
                    id = r.Id,
                    senderId = r.SenderId,
                    senderName = r.SenderName,
                    createdDate = r.CreatedDate
                })
                .ToListAsync();

            return Json(requests);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AcceptChatRequest(int requestId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "Không thể xác định người dùng." });
            }

            var request = await _context.ChatRequests.FindAsync(requestId);
            if (request == null || request.IsAccepted)
            {
                return Json(new { success = false, message = "Yêu cầu không tồn tại hoặc đã được xử lý." });
            }

            request.IsAccepted = true;
            request.ReceiverId = userId;
            request.AcceptedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var admin = await _userManager.FindByIdAsync(userId);
            await _hubContext.Clients.User(request.SenderId).SendAsync("ChatRequestAccepted", new
            {
                receiverId = userId,
                receiverName = admin?.FullName ?? "Admin",
                senderId = request.SenderId,
                senderName = request.SenderName
            });

            return Json(new { success = true, adminId = userId, adminName = admin?.FullName ?? "Admin" });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteChatRequests([FromBody] List<int> requestIds)
        {
            if (requestIds == null || !requestIds.Any())
            {
                return Json(new { success = false, message = "Không có yêu cầu nào được chọn để xóa." });
            }

            var requestsToDelete = await _context.ChatRequests
                .Where(r => requestIds.Contains(r.Id) && !r.IsAccepted)
                .ToListAsync();

            if (!requestsToDelete.Any())
            {
                return Json(new { success = false, message = "Không tìm thấy yêu cầu nào để xóa." });
            }

            _context.ChatRequests.RemoveRange(requestsToDelete);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã xóa các yêu cầu chat thành công." });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteChat(string receiverId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(receiverId))
            {
                return Json(new { success = false, message = "Thông tin người dùng không hợp lệ." });
            }

            var chatRequest = await _context.ChatRequests
                .FirstOrDefaultAsync(r => r.SenderId == receiverId && r.ReceiverId == userId);
            if (chatRequest != null)
            {
                _context.ChatRequests.Remove(chatRequest);
                await _context.SaveChangesAsync();
            }

            // Thông báo cho Brand rằng đoạn chat đã bị xóa
            await _hubContext.Clients.User(receiverId).SendAsync("ChatDeleted", userId);

            return Json(new { success = true, message = "Đã xóa liên kết chat thành công." });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAcceptedUsers()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { error = "Không thể xác định người dùng." });
            }

            var acceptedRequests = await _context.ChatRequests
                .Where(r => r.ReceiverId == userId && r.IsAccepted)
                .Select(r => new
                {
                    id = r.SenderId,
                    fullName = r.SenderName,
                    isOnline = _context.Users.Any(u => u.Id == r.SenderId && u.IsOnline)
                })
                .ToListAsync();

            return Json(acceptedRequests);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
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

            return Json(new { fullName = user.FullName ?? user.UserName ?? "Unknown" });
        }
    }
}