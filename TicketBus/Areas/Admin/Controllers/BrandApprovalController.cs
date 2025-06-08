using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TicketBus.Data;
using TicketBus.Models;

namespace TicketBus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BrandApprovalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BrandApprovalController> _logger;

        public BrandApprovalController(ApplicationDbContext context, ILogger<BrandApprovalController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: /Admin/BrandApproval/Index
        public async Task<IActionResult> Index(string filter = "pending")
        {
            // Thiết lập giá trị filter mặc định là "pending" (Chờ phê duyệt)
            ViewBag.Filter = filter;

            // Truy vấn cơ bản, khai báo rõ ràng là IQueryable<Brand>
            IQueryable<TicketBus.Models.Brand> query = _context.Brands
                .AsNoTracking()
                .Include(b => b.RegistForm)
                .Include(b => b.ApplicationUser);

            // Lọc theo trạng thái
            switch (filter)
            {
                case "approved":
                    query = query.Where(b => b.State == BrandState.HoatDong);
                    break;
                case "rejected":
                    query = query.Where(b => b.State == BrandState.KhongHoatDong);
                    break;
                case "pending":
                default:
                    query = query.Where(b => b.State == BrandState.ChoPheDuyet);
                    break;
            }

            // Sắp xếp và lấy danh sách
            var brands = await query
                .OrderBy(b => b.IdBrand)
                .ToListAsync();

            return View(brands);
        }

        // POST: /Admin/BrandApproval/ApproveBrand/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveBrand(int id)
        {
            var brand = await _context.Brands
                .Include(b => b.RegistForm)
                .Include(b => b.ApplicationUser)
                .FirstOrDefaultAsync(b => b.IdBrand == id);

            if (brand == null)
            {
                _logger.LogWarning("Brand with ID {Id} not found.", id);
                return Json(new { success = false, message = "Không tìm thấy hãng xe." });
            }

            if (brand.State != BrandState.ChoPheDuyet)
            {
                _logger.LogWarning("Brand {BrandId} is not in ChoPheDuyet state.", id);
                return Json(new { success = false, message = $"Hãng xe {brand.NameBrand} không ở trạng thái chờ phê duyệt." });
            }

            try
            {
                brand.State = BrandState.HoatDong;
                if (brand.RegistForm != null)
                {
                    brand.RegistForm.State = RegistFormState.DaXuLy;
                }
                else
                {
                    _logger.LogWarning("RegistForm for Brand {BrandId} is null.", id);
                }

                _context.Update(brand);
                await _context.SaveChangesAsync();

                // Lưu thông báo cho quản lý hãng xe
                var notification = new Notification
                {
                    UserId = brand.UserId,
                    Message = $"Hãng xe {brand.NameBrand} đã được phê duyệt thành công.",
                    CreatedDate = DateTime.Now,
                    IsRead = false
                };
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Brand {BrandId} approved successfully: NameBrand={NameBrand}",
                    id, brand.NameBrand);
                return Json(new { success = true, message = $"Đã phê duyệt hãng xe {brand.NameBrand} thành công!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to approve brand {BrandId}.", id);
                return Json(new { success = false, message = $"Có lỗi xảy ra khi phê duyệt hãng xe {brand.NameBrand}. Vui lòng thử lại." });
            }
        }

        // POST: /Admin/BrandApproval/RejectBrand/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectBrand(int id, string rejectReason)
        {
            var brand = await _context.Brands
                .Include(b => b.RegistForm)
                .Include(b => b.ApplicationUser)
                .FirstOrDefaultAsync(b => b.IdBrand == id);

            if (brand == null)
            {
                _logger.LogWarning("Brand with ID {Id} not found.", id);
                return Json(new { success = false, message = "Không tìm thấy hãng xe." });
            }

            if (brand.State != BrandState.ChoPheDuyet)
            {
                _logger.LogWarning("Brand {BrandId} is not in ChoPheDuyet state.", id);
                return Json(new { success = false, message = $"Hãng xe {brand.NameBrand} không ở trạng thái chờ phê duyệt." });
            }

            if (string.IsNullOrEmpty(rejectReason))
            {
                return Json(new { success = false, message = "Vui lòng nhập lý do từ chối." });
            }

            try
            {
                brand.State = BrandState.KhongHoatDong;
                if (brand.RegistForm != null)
                {
                    brand.RegistForm.State = RegistFormState.DaXuLy;
                    brand.RegistForm.RejectReason = rejectReason;
                }
                else
                {
                    _logger.LogWarning("RegistForm for Brand {BrandId} is null.", id);
                }

                _context.Update(brand);
                await _context.SaveChangesAsync();

                // Lưu thông báo cho quản lý hãng xe
                var notification = new Notification
                {
                    UserId = brand.UserId,
                    Message = $"Hãng xe {brand.NameBrand} đã bị từ chối. Lý do: {rejectReason}",
                    CreatedDate = DateTime.Now,
                    IsRead = false
                };
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Brand {BrandId} rejected successfully: NameBrand={NameBrand}, Reason={RejectReason}",
                    id, brand.NameBrand, rejectReason);
                return Json(new { success = true, message = $"Đã từ chối hãng xe {brand.NameBrand} thành công! Lý do: {rejectReason}" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reject brand {BrandId}.", id);
                return Json(new { success = false, message = $"Có lỗi xảy ra khi từ chối hãng xe {brand.NameBrand}. Vui lòng thử lại." });
            }
        }
    }
}