namespace TicketBus.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRead { get; set; }

        public ChatRoom ChatRoom { get; set; }
        public ApplicationUser Sender { get; set; }
        public ApplicationUser Receiver { get; set; }
    }
}
