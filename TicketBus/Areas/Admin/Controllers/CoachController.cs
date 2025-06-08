using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBus.Data;
using TicketBus.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace TicketBus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CoachController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CoachController> _logger;

        public CoachController(ApplicationDbContext context, ILogger<CoachController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: /Admin/Coach/PendingApproval
        public async Task<IActionResult> PendingApproval(string filter = "pending")
        {
            IQueryable<Coach> coachesQuery = _context.Coaches
                .Include(c => c.VehicleType)
                .Include(c => c.Brand);

            switch (filter)
            {
                case "approved":
                    coachesQuery = coachesQuery.Where(c => c.State == CoachState.DaPheDuyet);
                    break;
                case "rejected":
                    coachesQuery = coachesQuery.Where(c => c.State == CoachState.TuChoi);
                    break;
                case "active":
                    coachesQuery = coachesQuery.Where(c => c.State == CoachState.HoatDong);
                    break;
                case "inactive":
                    coachesQuery = coachesQuery.Where(c => c.State == CoachState.KhongHoatDong);
                    break;
                case "pending":
                default:
                    coachesQuery = coachesQuery.Where(c => c.State == CoachState.ChoPheDuyet);
                    break;
            }

            var coaches = await coachesQuery.ToListAsync();
            ViewBag.Filter = filter;

            return View(coaches);
        }

        // POST: /Admin/Coach/Approve/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var coach = await _context.Coaches
                .Include(c => c.Brand)
                .Include(c => c.VehicleType)
                .FirstOrDefaultAsync(c => c.IdCoach == id && c.State == CoachState.ChoPheDuyet);

            if (coach == null)
            {
                _logger.LogWarning("Approve: Coach with Id {IdCoach} not found or not in pending state.", id);
                return Json(new { success = false, message = "Không tìm thấy xe hoặc xe không ở trạng thái chờ phê duyệt." });
            }

            try
            {
                coach.State = CoachState.DaPheDuyet;
                _context.Coaches.Update(coach);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Approve: Coach {CoachCode} has been approved.", coach.CoachCode);

                // Sinh ghế tự động dựa trên SeatCount của VehicleType
                if (coach.VehicleType != null && coach.VehicleType.SeatCount > 0)
                {
                    // Xóa ghế cũ nếu có
                    var existingSeats = await _context.Seats
                        .Where(s => s.IdCoach == coach.IdCoach)
                        .ToListAsync();
                    if (existingSeats.Any())
                    {
                        _context.Seats.RemoveRange(existingSeats);
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Removed {SeatCount} existing seats for Coach {CoachCode}.", existingSeats.Count, coach.CoachCode);
                    }

                    // Sinh ghế mới
                    int seatCount = coach.VehicleType.SeatCount;
                    for (int i = 1; i <= seatCount; i++)
                    {
                        var seat = new Seat
                        {
                            SeatCode = $"S{i:D2}", // Ví dụ: S01, S02, ..., S34
                            SeatNumber = i,        // Số thứ tự ghế
                            State = SeatState.Trong, // Ghế mặc định là trống
                            IdCoach = coach.IdCoach // Liên kết với xe
                        };
                        _context.Seats.Add(seat);
                    }
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Generated {SeatCount} seats for Coach {CoachCode}.", seatCount, coach.CoachCode);
                }
                else
                {
                    _logger.LogWarning("VehicleType or SeatCount not found for Coach {CoachCode}.", coach.CoachCode);
                }

                // Tạo thông báo cho hãng xe
                var notification = new Notification
                {
                    UserId = coach.Brand.UserId,
                    Message = $"Xe {coach.CoachCode} ({coach.NumberPlate}) đã được phê duyệt và đã sinh {coach.VehicleType.SeatCount} ghế.",
                    CreatedDate = DateTime.Now,
                    IsRead = false
                };
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Phê duyệt xe thành công!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Approve: Failed to approve Coach {CoachCode}. Error: {Error}", coach.CoachCode, ex.Message);
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }

        // POST: /Admin/Coach/Reject/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id, string rejectReason)
        {
            var coach = await _context.Coaches
                .Include(c => c.Brand)
                .FirstOrDefaultAsync(c => c.IdCoach == id && c.State == CoachState.ChoPheDuyet);

            if (coach == null)
            {
                _logger.LogWarning("Reject: Coach with Id {IdCoach} not found or not in pending state.", id);
                return Json(new { success = false, message = "Không tìm thấy xe hoặc xe không ở trạng thái chờ phê duyệt." });
            }

            try
            {
                coach.State = CoachState.TuChoi;
                _context.Coaches.Update(coach);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Reject: Coach {CoachCode} has been rejected.", coach.CoachCode);

                // Tạo thông báo cho hãng xe
                var notification = new Notification
                {
                    UserId = coach.Brand.UserId,
                    Message = $"Xe {coach.CoachCode} ({coach.NumberPlate}) đã bị từ chối. Lý do: {rejectReason}",
                    CreatedDate = DateTime.Now,
                    IsRead = false
                };
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Từ chối xe thành công!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Reject: Failed to reject Coach {CoachCode}. Error: {Error}", coach.CoachCode, ex.Message);
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }

        // GET: /Admin/Coach/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var coach = await _context.Coaches
                .Include(c => c.VehicleType)
                .Include(c => c.Brand)
                .Include(c => c.Seats)
                .FirstOrDefaultAsync(c => c.IdCoach == id);

            if (coach == null)
            {
                return NotFound("Không tìm thấy xe.");
            }

            var images = System.Text.Json.JsonSerializer.Deserialize<List<string>>(coach.Images) ?? new List<string>();
            var documents = System.Text.Json.JsonSerializer.Deserialize<List<string>>(coach.Documents) ?? new List<string>();
            var seats = coach.Seats; // Sử dụng navigation property Seats

            ViewBag.Images = images;
            ViewBag.Documents = documents;
            ViewBag.Seats = seats;

            return View(coach);
        }

        // GET: /Admin/Coach/GenerateSeatsForApprovedCoaches
        [HttpGet]
        public async Task<IActionResult> GenerateSeatsForApprovedCoaches()
        {
            try
            {
                var approvedCoaches = await _context.Coaches
                    .Include(c => c.VehicleType)
                    .Where(c => c.State == CoachState.DaPheDuyet)
                    .ToListAsync();

                foreach (var coach in approvedCoaches)
                {
                    // Kiểm tra xem xe đã có ghế chưa
                    var existingSeats = await _context.Seats
                        .Where(s => s.IdCoach == coach.IdCoach)
                        .ToListAsync();

                    if (!existingSeats.Any() && coach.VehicleType != null && coach.VehicleType.SeatCount > 0)
                    {
                        int seatCount = coach.VehicleType.SeatCount;
                        for (int i = 1; i <= seatCount; i++)
                        {
                            var seat = new Seat
                            {
                                SeatCode = $"S{i:D2}",
                                SeatNumber = i,
                                State = SeatState.Trong,
                                IdCoach = coach.IdCoach
                            };
                            _context.Seats.Add(seat);
                        }
                        _logger.LogInformation("Generated {SeatCount} seats for Coach {CoachCode}.", seatCount, coach.CoachCode);
                    }
                }

                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Đã sinh ghế cho tất cả xe đã phê duyệt." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GenerateSeatsForApprovedCoaches: Failed to generate seats. Error: {Error}", ex.Message);
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }
    }
}