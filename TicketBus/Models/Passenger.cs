using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBus.Models
{
    public class Passenger
    {
        [Key]
        public int IdPassenger { get; set; }
        public string? PassengerCode { get; set; }
        public string? NamePassenger { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "PhoneNumber must contain only digits")]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? IdCard { get; set; }

        // Liên kết với ApplicationUser
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        public ICollection<Bill> Bills { get; set; } = new List<Bill>();

    }
}
