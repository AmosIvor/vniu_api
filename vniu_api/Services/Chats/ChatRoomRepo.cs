using AutoMapper;
using vniu_api.Exceptions;
using vniu_api.Repositories.Chats;
using vniu_api.Repositories;
using vniu_api.ViewModels.ChatsViewModels;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Chats;

namespace vniu_api.Services.Chats
{
    public class ChatRoomRepo : IChatRoomRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ChatRoomRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ChatRoomVM> CreateChatRoomByUserIdAsync(string userId)
        {
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == userId);

            if (!isUserExist)
            {
                throw new NotFoundException("User not found");
            }

            var chatroom = new ChatRoom()
            {
                UserId = userId
            };


            _context.ChatRooms.Add(chatroom);

            await _context.SaveChangesAsync();

            return _mapper.Map<ChatRoomVM>(chatroom);
        }

        public async Task<ChatRoomVM> GetChatRoomByUserIdAsync(string userId)
        {
            bool isUserExist = await IsChatRoomExist(userId);

            if (!isUserExist)
            {
                throw new NotFoundException("User not found");
            }

            var chatRoom = await _context.ChatRooms.SingleOrDefaultAsync(cr => cr.UserId == userId);

            var chatRoomVM = _mapper.Map<ChatRoomVM>(chatRoom);

            return chatRoomVM;
        }

        public async Task<ICollection<ChatRoomVM>> GetChatRoomsAsync()
        {
            var chatRooms = await _context.ChatRooms.OrderBy(cr => cr.ChatRoomId).ToListAsync();

            var chatRoomsVM = _mapper.Map<ICollection<ChatRoomVM>>(chatRooms);

            return chatRoomsVM;
        }

        public async Task<bool> IsChatRoomExist(string userId)
        {
            return await _context.ChatRooms.AnyAsync(cr => cr.UserId == userId);
        }
    }
}
