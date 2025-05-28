using Microsoft.AspNetCore.SignalR;
using TicketBus.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TicketBus.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace TicketBus.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<ChatHub> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task RequestChatWithRole(string senderId, string receiverRole)
        {
            try
            {
                _logger.LogInformation("Yêu cầu chat với Role - SenderId: {SenderId}, ReceiverRole: {ReceiverRole}", senderId, receiverRole);

                var sender = await _userManager.FindByIdAsync(senderId);
                if (sender == null)
                {
                    _logger.LogWarning("Người gửi không tồn tại - SenderId: {SenderId}", senderId);
                    return;
                }

                var chatRequest = new ChatRequest
                {
                    SenderId = senderId,
                    SenderName = sender.FullName ?? "Unknown",
                    ReceiverRole = receiverRole,
                    CreatedDate = DateTime.UtcNow,
                    IsAccepted = false
                };

                _context.ChatRequests.Add(chatRequest);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Tạo ChatRequest thành công - RequestId: {RequestId}", chatRequest.Id);

                var usersInRole = await _userManager.GetUsersInRoleAsync(receiverRole);
                foreach (var user in usersInRole.Where(u => u.IsOnline))
                {
                    await Clients.User(user.Id).SendAsync("ReceiveChatRequest", chatRequest.Id, senderId, chatRequest.SenderName, chatRequest.CreatedDate);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi yêu cầu chat với Role - SenderId: {SenderId}, ReceiverRole: {ReceiverRole}", senderId, receiverRole);
                throw;
            }
        }

        public async Task AcceptChatRequest(int requestId, string receiverId)
        {
            try
            {
                _logger.LogInformation("Chấp nhận yêu cầu chat - RequestId: {RequestId}, ReceiverId: {ReceiverId}", requestId, receiverId);

                var request = await _context.ChatRequests.FindAsync(requestId);
                if (request == null || request.IsAccepted)
                {
                    _logger.LogWarning("Yêu cầu không tồn tại hoặc đã được xử lý - RequestId: {RequestId}", requestId);
                    return;
                }

                request.IsAccepted = true;
                request.ReceiverId = receiverId;
                request.AcceptedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                var sender = await _userManager.FindByIdAsync(request.SenderId);
                var receiver = await _userManager.FindByIdAsync(receiverId);

                if (sender == null || receiver == null)
                {
                    _logger.LogWarning("Người gửi hoặc người nhận không tồn tại - SenderId: {SenderId}, ReceiverId: {ReceiverId}", request.SenderId, receiverId);
                    return;
                }

                await Clients.User(receiverId).SendAsync("ChatRequestHandled", requestId);
                await Clients.User(receiverId).SendAsync("StartChatWithUser", request.SenderId, request.SenderName);
                await Clients.User(request.SenderId).SendAsync("ChatRequestAccepted", new
                {
                    receiverId = receiverId,
                    receiverName = receiver.FullName ?? "Admin",
                    senderId = request.SenderId,
                    senderName = request.SenderName
                });
                _logger.LogInformation("Chấp nhận yêu cầu chat thành công - RequestId: {RequestId}", requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi chấp nhận yêu cầu chat - RequestId: {RequestId}, ReceiverId: {ReceiverId}", requestId, receiverId);
                throw;
            }
        }

        public async Task SendMessage(string senderId, string receiverId, string message)
        {
            try
            {
                _logger.LogInformation("Gửi tin nhắn - SenderId: {SenderId}, ReceiverId: {ReceiverId}, Message: {Message}", senderId, receiverId, message);

                var sender = await _userManager.FindByIdAsync(senderId);
                var receiver = await _userManager.FindByIdAsync(receiverId);
                if (sender == null || receiver == null)
                {
                    _logger.LogWarning("Người gửi hoặc người nhận không tồn tại - SenderId: {SenderId}, ReceiverId: {ReceiverId}", senderId, receiverId);
                    throw new ArgumentException("Người gửi hoặc người nhận không tồn tại trong hệ thống.");
                }

                var roomName = string.Join("-", new[] { senderId, receiverId }.OrderBy(x => x));
                _logger.LogInformation("RoomName được tạo: {RoomName}", roomName);

                var chatRoom = await _context.ChatRooms
                    .FirstOrDefaultAsync(r => r.RoomName == roomName && !r.IsDeleted);
                if (chatRoom == null)
                {
                    chatRoom = new ChatRoom
                    {
                        RoomName = roomName,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = senderId,
                        IsDeleted = false
                    };
                    _context.ChatRooms.Add(chatRoom);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Tạo mới ChatRoom thành công - RoomName: {RoomName}, ChatRoomId: {ChatRoomId}", roomName, chatRoom.Id);
                }

                var chatMessage = new ChatMessage
                {
                    ChatRoomId = chatRoom.Id,
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Content = message,
                    SentDate = DateTime.UtcNow,
                    IsRead = false
                };

                _context.ChatMessages.Add(chatMessage);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Lưu tin nhắn thành công - MessageId: {MessageId}, ChatRoomId: {ChatRoomId}", chatMessage.Id, chatRoom.Id);

                var senderName = sender.FullName ?? sender.UserName ?? "Unknown";

                // Gửi tin nhắn cho cả sender và receiver (kể cả khi offline)
                await Clients.User(senderId).SendAsync("ReceiveMessage", senderId, senderName, message, chatMessage.SentDate, chatMessage.IsRead);
                await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, senderName, message, chatMessage.SentDate, chatMessage.IsRead);

                // Cập nhật số tin nhắn chưa đọc cho receiver
                var unreadCount = await _context.ChatMessages
                    .Where(m => m.ReceiverId == receiverId && !m.IsRead)
                    .GroupBy(m => m.SenderId)
                    .Select(g => new { SenderId = g.Key, Count = g.Count() })
                    .ToListAsync();

                await Clients.User(receiverId).SendAsync("UpdateAdminUnreadCounts", unreadCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gửi tin nhắn - SenderId: {SenderId}, ReceiverId: {ReceiverId}", senderId, receiverId);
                throw new HubException($"Lỗi khi gửi tin nhắn: {ex.Message}");
            }
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.IsOnline = true;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Người dùng kết nối - UserId: {UserId}", userId);

                    // Đồng bộ tin nhắn chưa đọc khi kết nối
                    await SyncUnreadMessages(userId);
                }
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.IsOnline = false;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Người dùng ngắt kết nối - UserId: {UserId}", userId);
                }
            }
            await base.OnDisconnectedAsync(exception);
        }

        private async Task SyncUnreadMessages(string userId)
        {
            _logger.LogInformation("Đồng bộ tin nhắn chưa đọc - UserId: {UserId}", userId);

            var roomNames = await _context.ChatRooms
                .Where(r => r.RoomName.Contains(userId) && !r.IsDeleted)
                .Select(r => r.RoomName)
                .ToListAsync();

            foreach (var roomName in roomNames)
            {
                var chatRoom = await _context.ChatRooms
                    .Include(r => r.Messages)
                    .FirstOrDefaultAsync(r => r.RoomName == roomName);

                if (chatRoom != null)
                {
                    var unreadMessages = chatRoom.Messages
                        .Where(m => m.ReceiverId == userId && !m.IsRead)
                        .ToList();

                    if (unreadMessages.Any())
                    {
                        var unreadCountBySender = unreadMessages
                            .GroupBy(m => m.SenderId)
                            .Select(g => new { SenderId = g.Key, Count = g.Count() })
                            .ToList();

                        await Clients.User(userId).SendAsync("UpdateAdminUnreadCounts", unreadCountBySender);

                        foreach (var message in unreadMessages)
                        {
                            var sender = await _userManager.FindByIdAsync(message.SenderId);
                            var senderName = sender?.FullName ?? sender?.UserName ?? "Unknown";
                            await Clients.User(userId).SendAsync("ReceiveMessage", message.SenderId, senderName, message.Content, message.SentDate, message.IsRead);
                        }
                    }
                }
            }
        }

        public async Task MarkAsRead(string userId, string senderId)
        {
            try
            {
                _logger.LogInformation("Đánh dấu tin nhắn đã đọc - UserId: {UserId}, SenderId: {SenderId}", userId, senderId);

                var roomName = string.Join("-", new[] { userId, senderId }.OrderBy(x => x));
                var chatRoom = await _context.ChatRooms
                    .Include(r => r.Messages)
                    .FirstOrDefaultAsync(r => r.RoomName == roomName && !r.IsDeleted);

                if (chatRoom != null)
                {
                    var unreadMessages = chatRoom.Messages
                        .Where(m => m.ReceiverId == userId && !m.IsRead)
                        .ToList();

                    if (unreadMessages.Any())
                    {
                        foreach (var message in unreadMessages)
                        {
                            message.IsRead = true;
                        }
                        await _context.SaveChangesAsync();

                        var messageIds = unreadMessages.Select(m => m.Id).ToList();
                        await Clients.User(userId).SendAsync("ReceiveMessageReadStatus", senderId, messageIds);
                        await Clients.User(senderId).SendAsync("ReceiveMessageReadStatus", userId, messageIds);

                        // Cập nhật số tin nhắn chưa đọc cho userId
                        var newUnreadCountBySender = await _context.ChatMessages
                            .Where(m => m.ReceiverId == userId && !m.IsRead)
                            .GroupBy(m => m.SenderId)
                            .Select(g => new { SenderId = g.Key, Count = g.Count() })
                            .ToListAsync();

                        await Clients.User(userId).SendAsync("UpdateAdminUnreadCounts", newUnreadCountBySender);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đánh dấu tin nhắn đã đọc - UserId: {UserId}, SenderId: {SenderId}", userId, senderId);
                throw;
            }
        }

        public async Task CheckUnreadMessages(string userId)
        {
            try
            {
                _logger.LogInformation("Kiểm tra tin nhắn chưa đọc - UserId: {UserId}", userId);

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("Người dùng không tồn tại - UserId: {UserId}", userId);
                    return;
                }

                var unreadCountBySender = await _context.ChatMessages
                    .Where(m => m.ReceiverId == userId && !m.IsRead)
                    .GroupBy(m => m.SenderId)
                    .Select(g => new { SenderId = g.Key, Count = g.Count() })
                    .ToListAsync();

                await Clients.User(userId).SendAsync("UpdateAdminUnreadCounts", unreadCountBySender);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi kiểm tra tin nhắn chưa đọc - UserId: {UserId}", userId);
                throw;
            }
        }

        public async Task UserOnline(string userId)
        {
            try
            {
                _logger.LogInformation("Người dùng online - UserId: {UserId}", userId);
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.IsOnline = true;
                    await _context.SaveChangesAsync();
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        var acceptedRequests = await _context.ChatRequests
                            .Where(r => r.ReceiverId == userId && r.IsAccepted)
                            .Select(r => new
                            {
                                id = r.SenderId,
                                fullName = r.SenderName,
                                isOnline = _context.Users.Any(u => u.Id == r.SenderId && u.IsOnline)
                            })
                            .ToListAsync();
                        await Clients.User(userId).SendAsync("UpdateAcceptedUsers", acceptedRequests);
                    }
                    await SyncUnreadMessages(userId); // Đồng bộ tin nhắn khi online
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xử lý người dùng online - UserId: {UserId}", userId);
                throw;
            }
        }
    }
}