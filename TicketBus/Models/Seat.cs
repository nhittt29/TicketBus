using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace TicketBus.Models
{
    public class Seat
    {
        [Key]
        public int IdSeat { get; set; }
        public string? SeatCode { get; set; }
        public SeatState State { get; set; }
        public int SeatNumber { get; set; }
        [ForeignKey("Coach")]
        public int IdCoach { get; set; }

        public Coach? Coach { get; set; }
    }
    public enum SeatState
    {
        [Display(Name = "Trống")]
        Trong = 0,

        [Display(Name = "Đã đặt")]
        DaDat = 1
    }
}
