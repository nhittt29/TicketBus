using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace TicketBus.Models
{
    public class RouteStop
    {
        [Key]
        public int IdStop { get; set; }

        public string? StopCode { get; set; }

        [Required(ErrorMessage = "Tuyến xe là bắt buộc")]
        [ForeignKey("BusRoute")]
        public int IdRoute { get; set; }

        public string? StopName { get; set; }

        [Required(ErrorMessage = "Thành phố là bắt buộc")]
        [ForeignKey("City")]
        public int IdCity { get; set; }

        [Required(ErrorMessage = "Thứ tự điểm dừng là bắt buộc")]
        public int StopOrder { get; set; }

        public TimeSpan? Time { get; set; }

        public BusRoute? BusRoute { get; set; }
        public City? City { get; set; }
    }
}
