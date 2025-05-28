using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicketBus.Areas.Passenger.Controllers
{
    [Area("Passenger")]
    [Authorize(Roles = "Passenger")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
