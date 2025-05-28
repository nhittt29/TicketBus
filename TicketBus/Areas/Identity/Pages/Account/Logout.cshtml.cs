using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TicketBus.Models;

namespace TicketBus.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public void OnGet()
        {
            // Không cần xử lý gì ở OnGet, vì đăng xuất sẽ được xử lý qua POST
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var userDisplayName = user?.FullName ?? user?.Email ?? "Unknown";

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User {DisplayName} (Email: {Email}) logged out successfully.", userDisplayName, user?.Email ?? "Unknown");

            // Hiển thị thông báo đăng xuất thành công
            TempData["Message"] = $"Đăng xuất thành công! Tạm biệt {userDisplayName}.";

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
    }
}