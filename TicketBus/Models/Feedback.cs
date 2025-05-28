using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class Feedback
    {
        [Key]
        public int IdFeedback { get; set; }
        public string? FeedbackCode { get; set; }
        public string? Content { get; set; }
        public int? Rating { get; set; }
        public DateTime? CreatedAt { get; set; }
        [ForeignKey("Passenger")]
        public int? IdPassenger { get; set; }
        public Passenger? Passenger { get; set; }
    }
}
