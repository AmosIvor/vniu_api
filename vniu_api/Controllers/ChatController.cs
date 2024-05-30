using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vniu_api.Models.Responses;
using vniu_api.Repositories.Chats;
using vniu_api.ViewModels.ChatsViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRoomRepo _chatRoomRepo;
        private readonly IMessageRepo _messageRepo;

        public ChatController(IChatRoomRepo chatRoomRepo, IMessageRepo messageRepo)
        {
            _chatRoomRepo = chatRoomRepo;
            _messageRepo = messageRepo;
        }

        [HttpGet("chatrooms/get-all")]
        public async Task<IActionResult> GetChatRooms()
        {
            var chatRoomsVM = await _chatRoomRepo.GetChatRoomsAsync();

            return Ok(new SuccessResponse<ICollection<ChatRoomVM>>()
            {
                Message = "Get list chat room successfully",
                Data = chatRoomsVM
            });
        }

        [HttpGet("chatrooms/{userId}")]
        public async Task<IActionResult> GetChatRoomByUserId(string userId)
        {
            var chatRoomVM = await _chatRoomRepo.GetChatRoomByUserIdAsync(userId);

            return Ok(new SuccessResponse<ChatRoomVM>()
            {
                Message = "Get chat room by user successfully",
                Data = chatRoomVM
            });
        }

        [HttpGet("chatrooms/{userId}/messages")]
        public async Task<IActionResult> GetMessagesByUserId(string userId)
        {
            var messagesVM = await _messageRepo.GetMessagesByUserIdAsync(userId);

            return Ok(new SuccessResponse<ICollection<MessageVM>>()
            {
                Message = "Get all messages by user successfully",
                Data = messagesVM
            });
        }

        // If you uncomment this enpoint => error because it is duplicate endpoint below
        //[HttpPost("chatrooms/{userId}")]
        //public async Task<IActionResult> CreateChatRoom(string userId)
        //{
        //    var chatRoomVM = await _chatRoomRepo.CreateChatRoomByUserIdAsync(userId);

        //    return Ok(new SuccessResponse<ChatRoomVM>()
        //    {
        //        Message = "Create chat room by user successfully",
        //        Data = chatRoomVM
        //    });
        //}

        [HttpPost("chatrooms/{userId}/messages")]
        public async Task<IActionResult> CreateMessage(string userId, MessageVM messageVM)
        {
            var result = await _messageRepo.CreateMessageAsync(userId, messageVM);

            return Ok(new SuccessResponse<MessageVM>()
            {
                Message = "Create message by user successfully",
                Data = result
            });
        }

        [HttpPost("chatbot")]
        public async Task<IActionResult> ChatbotResponseMessage([FromBody] string userMessage)
        {
            var result = await _messageRepo.ChatbotResponseMessageAsync(userMessage);

            return Ok(new SuccessResponse<ChatbotVM>()
            {
                Message = "Chatbot response successfully",
                Data = result
            });
        }

        [HttpPost("messages/{messageId}")]
        public async Task<IActionResult> ReadMessage(int messageId)
        {
            var messageVM = await _messageRepo.ReadMesasgeAsync(messageId);

            return Ok(new SuccessResponse<MessageVM>()
            {
                Message = "Read message by user successfully",
                Data = messageVM
            });
        }
    }
}
