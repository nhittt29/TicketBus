using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TicketBus.Models.ViewModels
{
    public class DropOffViewModel
    {
        public string? DropOffName { get; set; }
        public int? IdCity { get; set; }
        public List<SelectListItem> Cities { get; set; } = new List<SelectListItem>();
    }
}