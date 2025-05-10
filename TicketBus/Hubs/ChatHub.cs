using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using TicketBus.Data;
using TicketBus.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TicketBus.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ChatHub> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ChatHub(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<ChatHub> logger, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
        }

        public async Task SendMessage(string senderId, string receiverId, string message)
        {
            _logger.LogInformation("Sending message - Sender: {SenderId}, Receiver: {ReceiverId}, Message: {Message}", senderId, receiverId, message);

            // Kiểm tra sender và receiver
            if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(receiverId) || string.IsNullOrEmpty(message))
            {
                _logger.LogWarning("Invalid message data - Sender: {SenderId}, Receiver: {ReceiverId}, Message: {Message}", senderId, receiverId, message);
                return;
            }

            var sender = await _userManager.FindByIdAsync(senderId);
            var receiver = await _userManager.FindByIdAsync(receiverId);

            if (sender == null || receiver == null)
            {
                _logger.LogWarning("Sender or Receiver not found - Sender: {SenderId}, Receiver: {ReceiverId}", senderId, receiverId);
                return;
            }

            // Kiểm tra role của sender
            var senderRoles = await _userManager.GetRolesAsync(sender);
            var isRestrictedRole = senderRoles.Any(r => new[] { "Brand", "Passenger", "NhanVien" }.Contains(r));
            var receiverRoles = await _userManager.GetRolesAsync(receiver);
            var isReceiverAdmin = receiverRoles.Any(r => r == "Admin");

            if (isRestrictedRole && !isReceiverAdmin)
            {
                _logger.LogWarning("Restricted user {SenderId} attempted to send message to non-Admin {ReceiverId}", senderId, receiverId);
                return; // Chỉ cho phép gửi đến Admin nếu là Brand, Passenger, hoặc NhanVien
            }

            // Tạo hoặc lấy chat room
            var roomName = string.Join("-", new[] { senderId, receiverId }.OrderBy(x => x));
            var chatRoom = await _context.ChatRooms
                .Include(r => r.Messages)
                .FirstOrDefaultAsync(r => r.RoomName == roomName) ?? new ChatRoom
                {
                    RoomName = roomName,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = senderId
                };

            if (chatRoom.Id == 0) // Phòng mới
            {
                _context.ChatRooms.Add(chatRoom);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Created new chat room - RoomName: {RoomName}, ChatRoomId: {ChatRoomId}", roomName, chatRoom.Id);
            }

            // Tạo tin nhắn mới
            var chatMessage = new ChatMessage
            {
                ChatRoomId = chatRoom.Id,
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = message,
                SentDate = DateTime.UtcNow,
                IsRead = false // Đảm bảo tin nhắn mới có IsRead = false
            };

            chatRoom.Messages.Add(chatMessage);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Message saved - MessageId: {MessageId}, ChatRoomId: {ChatRoomId}, IsRead: {IsRead}", chatMessage.Id, chatRoom.Id, chatMessage.IsRead);

            var senderName = sender.FullName ?? "Unknown";
            var sentDate = chatMessage.SentDate;

            // Gửi tin nhắn đến cả sender và receiver
            _logger.LogInformation("Sending message to receiverId: {ReceiverId} and senderId: {SenderId}", receiverId, senderId);
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, senderName, message, sentDate);
            await Clients.User(senderId).SendAsync("ReceiveMessage", senderId, senderName, message, sentDate);

            _logger.LogInformation("Message sent successfully to Receiver: {ReceiverId} and Sender: {SenderId}", receiverId, senderId);
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            _logger.LogInformation("OnConnectedAsync - Context.UserIdentifier: {UserIdentifier}", userId);
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.IsOnline = true;
                    await _context.SaveChangesAsync();
                    await Clients.Others.SendAsync("UserStatusChanged", userId, true);
                    _logger.LogInformation("User {UserId} connected with Context.UserIdentifier: {UserIdentifier}", userId, Context.UserIdentifier);
                }
                else
                {
                    _logger.LogWarning("User not found for UserIdentifier: {UserIdentifier}", userId);
                }
            }
            else
            {
                _logger.LogWarning("Context.UserIdentifier is null on connection");
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            _logger.LogInformation("OnDisconnectedAsync - Context.UserIdentifier: {UserIdentifier}", userId);
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.IsOnline = false;
                    await _context.SaveChangesAsync();
                    await Clients.Others.SendAsync("UserStatusChanged", userId, false);
                    _logger.LogInformation("User {UserId} disconnected.", userId);
                }
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}