using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class ScheduleDetails
    {
        [Key]
        public int IdSchedule { get; set; }

        [Required(ErrorMessage = "Xe là bắt buộc")]
        [ForeignKey("Coach")]
        public int IdCoach { get; set; }

        [Required(ErrorMessage = "Tuyến xe là bắt buộc")]
        [ForeignKey("BusRoute")]
        public int IdRoute { get; set; }

        [Required(ErrorMessage = "Giờ khởi hành là bắt buộc")]
        public TimeSpan DepartTime { get; set; }

        [Required(ErrorMessage = "Giờ đến là bắt buộc")]
        public TimeSpan ArriveTime { get; set; }

        public Coach Coach { get; set; }
        public BusRoute BusRoute { get; set; }

        public List<Price>? Prices { get; set; }
    }
}
