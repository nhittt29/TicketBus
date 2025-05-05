using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketBus.Data;
using TicketBus.Models;
using TicketBus.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Đăng ký DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký Identity sử dụng ApplicationUser
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Cấu hình cookie xác thực
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = ".AspNetCore.Identity.Application";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.ExpireTimeSpan = TimeSpan.FromDays(14); // Cookie tồn tại 14 ngày khi RememberMe = true
    options.SlidingExpiration = true; // Gia hạn cookie nếu người dùng hoạt động
});

// Đăng ký Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Chuyển hướng /Admin đến /Admin/Home/Index
app.MapGet("/Admin", context =>
{
    context.Response.Redirect("/Admin/Home/AdminPanel");
    return Task.CompletedTask;
});

// Định tuyến cho Areas
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Thêm để Identity UI (Razor Pages) hoạt động

// Tạo vai trò và tài khoản mặc định
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    // Áp dụng migration
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();

    // Seeding vai trò
    string[] roleNames = { "Admin", "NhanVien", "Brand", "Passenger" };
    foreach (var roleName in roleNames)
    {
        try
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    logger.LogInformation("Role {RoleName} created successfully.", roleName);
                }
                else
                {
                    logger.LogError("Failed to create role {RoleName}: {Errors}", roleName, string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                logger.LogInformation("Role {RoleName} already exists.", roleName);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating role {RoleName}.", roleName);
        }
    }

    // Seeding tài khoản Admin
    var adminEmail = "admin@gmail.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FullName = "Quản trị viên",
            PhoneNumber = "0000000000" // Giá trị mặc định cho PhoneNumber
        };
        var createResult = await userManager.CreateAsync(adminUser, "Admin123/");
        if (createResult.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
            logger.LogInformation("Default Admin account created with email {Email}.", adminEmail);
        }
        else
        {
            logger.LogError("Failed to create Admin account {Email}: {Errors}", adminEmail, string.Join(", ", createResult.Errors.Select(e => e.Description)));
        }
    }
    else
    {
        logger.LogInformation("Admin account {Email} already exists.", adminEmail);
    }

    // Seeding tài khoản NhanVien
    var nhanVienEmail = "nhanvien@gmail.com";
    var nhanVienUser = await userManager.FindByEmailAsync(nhanVienEmail);
    if (nhanVienUser == null)
    {
        nhanVienUser = new ApplicationUser
        {
            UserName = nhanVienEmail,
            Email = nhanVienEmail,
            FullName = "Nhân viên",
            PhoneNumber = "0000000000" // Giá trị mặc định cho PhoneNumber
        };
        var createResult = await userManager.CreateAsync(nhanVienUser, "Nhanvien123/");
        if (createResult.Succeeded)
        {
            await userManager.AddToRoleAsync(nhanVienUser, "NhanVien");
            logger.LogInformation("Default NhanVien account created with email {Email}.", nhanVienEmail);
        }
        else
        {
            logger.LogError("Failed to create NhanVien account {Email}: {Errors}", nhanVienEmail, string.Join(", ", createResult.Errors.Select(e => e.Description)));
        }
    }
    else
    {
        logger.LogInformation("NhanVien account {Email} already exists.", nhanVienEmail);
    }
}

app.Run();