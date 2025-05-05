using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.ChatMessageFeature.Commands.ChatMessageSend
{
    internal class ChatMessageSendCommandValidator : AbstractValidator<ChatMessageSendCommand>
    {
        public ChatMessageSendCommandValidator()
        {
            RuleFor(s=>s.SenderAppUserId).NotEmpty().MaximumLength(33);
            RuleFor(s => s.ReceiverAppUserId).NotEmpty().MaximumLength(33);
            RuleFor(s => s.Message).NotEmpty().MaximumLength(400);
        }
    }
}
