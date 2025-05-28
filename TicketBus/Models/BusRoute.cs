using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace TicketBus.Models
{
    public class BusRoute
    {
        [Key]
        public int IdRoute { get; set; }

        public string? RouteCode { get; set; }

        public string? NameRoute { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Distance must be greater than 0")]
        public int? Distance { get; set; }

        [ForeignKey("Brand")]
        public int IdBrand { get; set; }

        [ForeignKey("RegistForm")]
        public int? IdRegist { get; set; }

        [ForeignKey("StartCity")]
        public int? IdStartCity { get; set; }

        [ForeignKey("EndCity")]
        public int? IdEndCity { get; set; }

        public BusRouteState State { get; set; }

        public TimeSpan? TravelTime { get; set; }

        public DateTime? StartDate { get; set; }

        public Brand Brand { get; set; }
        public RegistForm? RegistForm { get; set; }
        public City? StartCity { get; set; }
        public City? EndCity { get; set; }
        public List<RouteStop> RouteStops { get; set; } = new List<RouteStop>();
        public List<Pickup> Pickups { get; set; } = new List<Pickup>();
        public List<DropOff> DropOffs { get; set; } = new List<DropOff>();
    }
    public enum BusRouteState
    {
        [Display(Name = "Chờ phê duyệt")]
        ChoPheDuyet = 0,

        [Display(Name = "Đã phê duyệt")]
        DaPheDuyet = 1,

        [Display(Name = "Từ chối")]
        TuChoi = 2,

        [Display(Name = "Không hoạt động")]
        KhongHoatDong = 3
    }
}
