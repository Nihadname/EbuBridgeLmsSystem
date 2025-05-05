using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.ChatMessageFeature.Commands.ChatMessageSend
{
    public sealed record ChatMessageSendCommand:IRequest<Result<Unit>>
    {
        public string SenderAppUserId { get; init; }
        public string ReceiverAppUserId { get; init; }
        public string Message { get; init; }
    }
}
