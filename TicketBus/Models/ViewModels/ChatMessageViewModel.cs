namespace TicketBus.Models.ViewModels
{
    public class ChatMessageViewModel
    {
        public string senderId { get; set; }
        public string senderName { get; set; }
        public string content { get; set; }
        public DateTime sentDate { get; set; }
        public bool isRead { get; set; }
    }
}
