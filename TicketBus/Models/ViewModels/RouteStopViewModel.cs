using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models.ViewModels
{
    public class RouteStopViewModel
    {
        public string? StopCode { get; set; }

        public string? StopName { get; set; }

        public int? IdCity { get; set; }

        public int StopOrder { get; set; }

        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Thời gian phải có định dạng HH:mm (ví dụ: 14:30)")]
        public string? Time { get; set; }

        public List<SelectListItem> Cities { get; set; } = new List<SelectListItem>();
    }
}