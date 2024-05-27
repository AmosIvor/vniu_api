using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using vniu_api.Exceptions;
using vniu_api.Models.EF.Chats;
using vniu_api.Repositories.Chats;
using vniu_api.Repositories;
using vniu_api.ViewModels.ChatsViewModels;
using vniu_api.Hubs;
using Microsoft.EntityFrameworkCore;

namespace vniu_api.Services.Chats
{
    public class MessageRepo : IMessageRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub, IChatClient> _hub;

        public MessageRepo(DataContext context, IMapper mapper, IHubContext<ChatHub, IChatClient> hub)
        {
            _context = context;
            _mapper = mapper;
            _hub = hub;
        }

        public async Task<MessageVM> CreateMessageAsync(string userId, MessageVM messageVM)
        {
            // check exist user
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == userId);

            if (!isUserExist)
            {
                throw new NotFoundException("User not found");
            }

            var chatroom = await _context.ChatRooms.SingleOrDefaultAsync(cr => cr.UserId == userId);

            if (chatroom == null)
            {
                throw new NotFoundException("Chat room not found");
            }

            var message = _mapper.Map<Message>(messageVM);

            message.MessageCreateAt = DateTime.Now;

            message.ChatRoomId = chatroom.ChatRoomId;

            _context.Messages.Add(message);

            await _context.SaveChangesAsync();

            var messageVMResult = _mapper.Map<MessageVM>(message);

            // send message
            await _hub.Clients.All.ReceiveMessage(messageVMResult);

            return messageVMResult;
        }

        public async Task<ICollection<MessageVM>> GetMessagesByUserIdAsync(string userId)
        {
            var chatRoom = await _context.ChatRooms.SingleOrDefaultAsync(cr => cr.UserId == userId);

            if (chatRoom == null)
            {
                throw new NotFoundException("Chat room not found");
            }

            var messages = await _context.Messages.Where(m => m.ChatRoomId == chatRoom.ChatRoomId).OrderByDescending(m => m.MessageCreateAt).ToListAsync();

            var messagesVM = _mapper.Map<ICollection<MessageVM>>(messages);

            return messagesVM;
        }

        public async Task<MessageVM> ReadMesasgeAsync(int messageId)
        {
            var message = await _context.Messages.SingleOrDefaultAsync(m => m.MessageId == messageId);

            if (message == null)
            {
                throw new NotFoundException("Message not found");
            }

            message.IsRead = true;
            message.MessageCreateAt = DateTime.Now;

            _context.Messages.Update(message);

            await _context.SaveChangesAsync();

            var messageVM = _mapper.Map<MessageVM>(message);

            return messageVM;
        }
    }
}
