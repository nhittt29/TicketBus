using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class ChatRoom
    {
        [Key]
        public int Id { get; set; }
        public string RoomName { get; set; } // Tên phòng chat (ví dụ: Admin-Employee-1)
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } // ID của người tạo phòng

        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>(); // Danh sách tin nhắn trong phòng chat
        public List<ApplicationUser> Participants { get; set; } // Quan hệ với ApplicationUser
        public bool IsDeleted { get; set; } = false;
    }
}
