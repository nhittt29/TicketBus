using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBus.Data;
using TicketBus.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using TicketBus.Models.ViewModels;

namespace TicketBus.Areas.Brand.Controllers
{
    [Area("Brand")]
    [Authorize(Roles = "Brand")]
    public class CoachController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CoachController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;

        public CoachController(ApplicationDbContext context, ILogger<CoachController> logger, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _environment = environment;
            _userManager = userManager;
        }

        // GET: /Brand/Coach/Register
        public IActionResult Register()
        {
            ViewBag.VehicleTypes = _context.VehicleTypes
                .Where(v => v.State == VehicleTypeState.HoatDong)
                .ToList();
            return View();
        }

        // POST: /Brand/Coach/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CoachViewModel viewModel)
        {
            _logger.LogInformation("Register POST: Received data - CoachCode: {CoachCode}, NumberPlate: {NumberPlate}, IdType: {IdType}, IdBrand: {IdBrand}, State: {State}",
                viewModel.CoachCode, viewModel.NumberPlate, viewModel.IdType, viewModel.IdBrand, viewModel.State);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("Register POST: User not found.");
                return Json(new { success = false, message = "Không thể xác định người dùng. Vui lòng đăng nhập lại." });
            }

            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.UserId == user.Id);
            if (brand == null)
            {
                _logger.LogWarning("Register POST: Brand not found for UserId {UserId}.", user.Id);
                return Json(new { success = false, message = "Không tìm thấy hãng xe." });
            }

            try
            {
                var imagePaths = new List<string>();
                var documentPaths = new List<string>();

                var imagesFolder = Path.Combine(_environment.WebRootPath, "images", "coaches");
                var documentsFolder = Path.Combine(_environment.WebRootPath, "documents", "coaches");

                if (!Directory.Exists(imagesFolder)) Directory.CreateDirectory(imagesFolder);
                if (!Directory.Exists(documentsFolder)) Directory.CreateDirectory(documentsFolder);

                if (viewModel.ImageList != null && viewModel.ImageList.Any())
                {
                    foreach (var image in viewModel.ImageList.Where(img => img != null && img.Length > 0))
                    {
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                        var filePath = Path.Combine(imagesFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }
                        imagePaths.Add($"/images/coaches/{uniqueFileName}");
                    }
                }

                if (viewModel.DocumentList != null && viewModel.DocumentList.Any())
                {
                    foreach (var doc in viewModel.DocumentList.Where(doc => doc != null && doc.Length > 0))
                    {
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + doc.FileName;
                        var filePath = Path.Combine(documentsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await doc.CopyToAsync(fileStream);
                        }
                        documentPaths.Add($"/documents/coaches/{uniqueFileName}");
                    }
                }

                var imagesJson = JsonSerializer.Serialize(imagePaths);
                var documentsJson = JsonSerializer.Serialize(documentPaths);

                var coach = new Coach
                {
                    CoachCode = viewModel.CoachCode,
                    NumberPlate = viewModel.NumberPlate,
                    IdType = viewModel.IdType,
                    IdBrand = brand.IdBrand,
                    State = CoachState.ChoPheDuyet,
                    Images = imagesJson,
                    Documents = documentsJson
                };

                _context.Coaches.Add(coach);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Register POST: Successfully saved Coach {CoachCode}.", coach.CoachCode);

                var notification = new Notification
                {
                    UserId = user.Id,
                    Message = $"Xe {coach.CoachCode} ({coach.NumberPlate}) đã được đăng ký và đang chờ phê duyệt.",
                    CreatedDate = DateTime.Now,
                    IsRead = false
                };
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Đăng ký xe thành công!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Register POST: Failed to save Coach. Error: {Error}", ex.Message);
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }
    }
}