using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models.ViewModels
{
    public class BusRouteViewModel
    {
        public string? RouteCode { get; set; }

        [Required(ErrorMessage = "Tên tuyến là bắt buộc")]
        public string? NameRoute { get; set; }

        public string? Address { get; set; }

        [Required(ErrorMessage = "Khoảng cách là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "Khoảng cách phải lớn hơn 0")]
        public int Distance { get; set; }

        [Required(ErrorMessage = "Hãng xe là bắt buộc")]
        public int IdBrand { get; set; }

        public int? IdRegist { get; set; }

        [Required(ErrorMessage = "Thành phố xuất phát là bắt buộc")]
        public int? IdStartCity { get; set; }

        [Required(ErrorMessage = "Thành phố kết thúc là bắt buộc")]
        public int? IdEndCity { get; set; }

        public BusRouteState State { get; set; }

        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Thời gian hành trình phải có định dạng HH:mm (ví dụ: 07:00)")]
        public string? TravelTime { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu hoạt động là bắt buộc")]
        public DateTime? StartDate { get; set; }

        public List<RouteStopViewModel> RouteStops { get; set; } = new List<RouteStopViewModel>();

        public List<PickupViewModel> Pickups { get; set; } = new List<PickupViewModel>();

        public List<DropOffViewModel> DropOffs { get; set; } = new List<DropOffViewModel>();

        public List<SelectListItem> Brands { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Cities { get; set; } = new List<SelectListItem>();
    }
}