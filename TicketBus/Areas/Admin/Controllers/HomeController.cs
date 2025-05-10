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

namespace TicketBus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, NhanVien, Passenger")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /Admin/Home/ChatIndex
        public async Task<IActionResult> ChatIndex()
        {
            return await AdminPanel();
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

            // Lấy tổng số thông báo
            var notificationsCount = await _context.Notifications
                .CountAsync();

            // Lấy danh sách thông báo mới nhất (giới hạn 5 thông báo)
            var notifications = await _context.Notifications
                .OrderByDescending(n => n.CreatedDate)
                .Take(5)
                .ToListAsync();

            // Dữ liệu cho Area Chart 1: Số tuyến xe theo trạng thái
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

            // Dữ liệu cho Area Chart 2: Số thông báo theo tháng (6 tháng gần nhất)
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

            // Dữ liệu cho Bar Chart: Số xe theo hãng xe
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

            // Dữ liệu cho Pie Chart: Tỷ lệ thông báo đã đọc/chưa đọc
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

            // Dữ liệu cho Scatter Chart: Giá vé trung bình theo khoảng cách tuyến xe
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
            ViewBag.NotificationsCount = notificationsCount;
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

            return View("AdminPanel");
        }

        // GET: /Admin/Home/Chat
        public IActionResult Chat()
        {
            return View("Chat");
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

            var users = isRestrictedRole
                ? (await _userManager.GetUsersInRoleAsync("Admin")).Select(u => new
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
                return Json(new { error = "Thông tin người dùng không hợp lệ." });
            }

            var roomName = string.Join("-", new[] { userId, receiverId }.OrderBy(x => x));
            var chatRoom = await _context.ChatRooms
                .Include(r => r.Messages)
                .FirstOrDefaultAsync(r => r.RoomName == roomName);

            if (chatRoom == null)
            {
                return Json(new List<ChatMessageViewModel>());
            }

            // Không tự động cập nhật IsRead ở đây, chỉ lấy dữ liệu
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); // Windows
            // Nếu dùng Linux, thay bằng: TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh");

            var messages = chatRoom.Messages
                .OrderBy(m => m.SentDate) // Sắp xếp theo thời gian gửi
                .Select(m => new ChatMessageViewModel
                {
                    senderId = m.SenderId,
                    senderName = _context.Users.FirstOrDefault(u => u.Id == m.SenderId)?.FullName ?? "Unknown",
                    content = m.Content,
                    sentDate = TimeZoneInfo.ConvertTimeFromUtc(m.SentDate, vietnamTimeZone), // Chuyển đổi sang múi giờ Việt Nam
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

            var roomName = string.Join("-", new[] { userId, receiverId }.OrderBy(x => x));
            var chatRoom = await _context.ChatRooms
                .Include(r => r.Messages)
                .FirstOrDefaultAsync(r => r.RoomName == roomName);

            if (chatRoom == null)
            {
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
                }
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true });
        }
    }
}