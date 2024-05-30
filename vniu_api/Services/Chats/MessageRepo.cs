using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using vniu_api.Exceptions;
using vniu_api.Models.EF.Chats;
using vniu_api.Repositories.Chats;
using vniu_api.Repositories;
using vniu_api.ViewModels.ChatsViewModels;
using vniu_api.Hubs;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text;

namespace vniu_api.Services.Chats
{
    public class MessageRepo : IMessageRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub, IChatClient> _hub;

        public MessageRepo(DataContext context, IMapper mapper, 
                            IHubContext<ChatHub, IChatClient> hub)
        {
            _context = context;
            _mapper = mapper;
            _hub = hub;
        }

        public async Task<ChatbotVM> ChatbotResponseMessageAsync(string userMessage)
        {
            var httpClient = new HttpClient();
            var apiChatbot = "https://vniuchatbot.azurewebsites.net/webhooks/rest/webhook";

            // Create a request body
            var requestBody = new
            {
                message = userMessage
            };

            var jsonRequestBody = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            // Send the POST request
            var response = await httpClient.PostAsync(apiChatbot, content);
            response.EnsureSuccessStatusCode();

            // Read the response content
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var responseObjects = JsonSerializer.Deserialize<ChatbotServerResponseVM[]>(jsonResponse);

            if (responseObjects == null || responseObjects.Length == 0)
            {
                throw new Exception("No response from chatbot rasa");
            }

            string? firstText = null;
            string? firstImage = null;

            foreach (var responseObject in responseObjects)
            {
                if (firstText == null && !string.IsNullOrEmpty(responseObject.text))
                {
                    firstText = responseObject.text;
                }

                if (firstImage == null && !string.IsNullOrEmpty(responseObject.image))
                {
                    firstImage = responseObject.image;
                }

                if (firstText != null && firstImage != null)
                {
                    break;
                }
            }

            var chatbotVM = new ChatbotVM
            {
                ChatbotId = Guid.NewGuid().ToString(),
                ChatbotContent = firstText!,
                ImageUrl = firstImage,
                IsFromUser = false,
                MessageCreateAt = DateTime.Now,
            };

            return chatbotVM;
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
