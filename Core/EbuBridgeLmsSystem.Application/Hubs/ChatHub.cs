using EbuBridgeLmsSystem.Application.Features.ChatMessageFeature.Commands.ChatMessageSend;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EbuBridgeLmsSystem.Application.Hubs
{
    public sealed class ChatHub:Hub
    {
        private readonly ISender _sender;

        public ChatHub(ISender sender)
        {
            _sender = sender;
        }

        public async Task SendMessage(string receiverId, string message)
        {
            var senderId = Context.UserIdentifier;
            if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(receiverId) || string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Sender, receiver, or message cannot be null or empty.");
            }

            var newChatMessageCommand = new ChatMessageSendCommand()
            {
                Message = message,
                ReceiverAppUserId = receiverId,
                SenderAppUserId = senderId,

            };
            var result=await _sender.Send(newChatMessageCommand);
            if (!result.IsSuccess)
            {
                throw new ArgumentException(nameof(result));
            }
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, message);
        }
    }
}
