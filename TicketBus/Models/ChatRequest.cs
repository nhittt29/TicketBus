using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBus.Models
{
    public class ChatRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SenderId { get; set; }

        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; }

        public string SenderName { get; set; }

        public string? ReceiverId { get; set; }

        [ForeignKey("ReceiverId")]
        public ApplicationUser? Receiver { get; set; }

        [Required]
        public string ReceiverRole { get; set; }

        public bool IsAccepted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? AcceptedDate { get; set; }
    }
}
