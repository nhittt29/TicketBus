﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketBus.Data;
using TicketBus.Models;

namespace TicketBus.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
            [StringLength(100, ErrorMessage = "Họ và tên phải từ {2} đến {1} ký tự.", MinimumLength = 2)]
            [Display(Name = "Họ và tên")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập email.")]
            [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
            [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
            [Display(Name = "Số điện thoại")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Vui lòng chọn ngày sinh.")]
            [DataType(DataType.Date)]
            [Display(Name = "Ngày sinh")]
            public DateTime DateOfBirth { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
            [StringLength(100, ErrorMessage = "Mật khẩu phải từ {2} đến {1} ký tự.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Mật khẩu")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Xác nhận mật khẩu")]
            [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Vui lòng chọn vai trò.")]
            [Display(Name = "Vai trò")]
            public string Role { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            // Log dữ liệu đầu vào
            _logger.LogInformation("Register attempt with data: FullName={FullName}, Email={Email}, PhoneNumber={PhoneNumber}, DateOfBirth={DateOfBirth}, Role={Role}",
                Input.FullName, Input.Email, Input.PhoneNumber, Input.DateOfBirth, Input.Role);

            if (ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is valid. Proceeding with registration.");

                var user = CreateUser();
                user.FullName = Input.FullName;
                user.PhoneNumber = Input.PhoneNumber;
                user.DateOfBirth = Input.DateOfBirth;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                // Tạo user
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created successfully: Email={Email}", Input.Email);

                    // Kiểm tra và tạo vai trò nếu chưa tồn tại
                    if (!string.IsNullOrEmpty(Input.Role))
                    {
                        if (!await _roleManager.RoleExistsAsync(Input.Role))
                        {
                            var role = new IdentityRole(Input.Role);
                            var roleResult = await _roleManager.CreateAsync(role);
                            if (!roleResult.Succeeded)
                            {
                                _logger.LogWarning("Failed to create role {Role}. Errors: {Errors}",
                                    Input.Role, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                                ModelState.AddModelError(string.Empty, "Không thể tạo vai trò. Vui lòng liên hệ quản trị viên.");
                                return Page();
                            }
                            _logger.LogInformation("Created role {Role}", Input.Role);
                        }

                        // Gán vai trò cho user
                        var addRoleResult = await _userManager.AddToRoleAsync(user, Input.Role);
                        if (!addRoleResult.Succeeded)
                        {
                            _logger.LogWarning("Failed to assign role {Role} to user {Email}. Errors: {Errors}",
                                Input.Role, Input.Email, string.Join(", ", addRoleResult.Errors.Select(e => e.Description)));
                            foreach (var error in addRoleResult.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return Page();
                        }
                        _logger.LogInformation("Role {Role} assigned to user {Email}", Input.Role, Input.Email);
                    }

                    // Đăng nhập user
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // Hiển thị thông báo đăng ký thành công
                    var displayName = user.FullName ?? user.Email;
                    TempData["Message"] = $"Đăng ký thành công! Chào mừng {displayName}.";

                    // Điều hướng về trang chủ cho tất cả vai trò
                    returnUrl = "/";
                    _logger.LogInformation("Redirecting user {Email} to {ReturnUrl}", Input.Email, returnUrl);
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    _logger.LogWarning("User creation failed for {Email}. Errors: {Errors}",
                        Input.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                // Log các lỗi validation
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _logger.LogWarning("ModelState is invalid. Errors: {Errors}", string.Join(", ", errors));
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            // If we got this far, something failed, redisplay form
            _logger.LogInformation("Redisplaying form due to validation errors.");
            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}