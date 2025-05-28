namespace TicketBus.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; }

        public ApplicationUser? User { get; set; }
    }
}
