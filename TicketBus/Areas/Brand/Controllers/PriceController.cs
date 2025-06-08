using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketBus.Data;
using TicketBus.Models;

namespace TicketBus.Areas.Brand.Controllers
{
    [Area("Brand")]
    [Authorize(Roles = "Brand")]
    public class PriceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PriceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Brand/Price/Index
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (brand == null)
            {
                return NotFound("Không tìm thấy hãng xe.");
            }

            var prices = await _context.Prices
                .Include(p => p.RouteStopStart)
                .Include(p => p.RouteStopEnd)
                .Include(p => p.ScheduleDetails)
                .ThenInclude(s => s.BusRoute)
                .Include(p => p.ScheduleDetails)
                .ThenInclude(s => s.Coach)
                .Where(p => p.ScheduleDetails.BusRoute.IdBrand == brand.IdBrand)
                .ToListAsync();

            return View(prices);
        }
    }
}