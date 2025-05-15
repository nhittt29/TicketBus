using Microsoft.AspNetCore.Identity;

namespace TicketBus.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }

        //Khung Chat
        public bool IsOnline { get; set; }
        public List<ChatRoom> ChatRooms { get; set; }
    }
}
