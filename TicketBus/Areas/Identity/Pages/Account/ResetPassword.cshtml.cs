using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using TicketBus.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace TicketBus.Areas.Identity.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới.")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "Mật khẩu phải dài ít nhất {2} và tối đa {1} ký tự.", MinimumLength = 6)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Xác nhận mật khẩu")]
            [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
            public string ConfirmPassword { get; set; }
        }

        public IActionResult OnGet()
        {
            Email = TempData["ResetEmail"] as string;
            if (string.IsNullOrEmpty(Email))
            {
                TempData["ErrorMessage"] = "Không tìm thấy email. Vui lòng thử lại từ đầu.";
                return RedirectToPage("./ForgotPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (string.IsNullOrEmpty(Email))
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy email. Vui lòng thử lại từ đầu.");
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email không tồn tại.");
                return Page();
            }

            var result = await _userManager.RemovePasswordAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddPasswordAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    TempData["Message"] = "Mật khẩu đã được đặt lại thành công. Vui lòng đăng nhập.";
                    return RedirectToPage("./Login");
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}