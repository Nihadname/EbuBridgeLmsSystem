using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EbuBridgeLmsSystem.Application.Features.ChatMessageFeature.Commands.ChatMessageSend
{
    public sealed class ChatMessageSendHandler : IRequestHandler<ChatMessageSendCommand, Result<Unit>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserResolver _userResolver;
        public ChatMessageSendHandler(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IAppUserResolver userResolver)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userResolver = userResolver;
        }

        public async Task<Result<Unit>> Handle(ChatMessageSendCommand request, CancellationToken cancellationToken)
        {
            var userId=_userResolver.UserId;
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Result<Unit>.Failure(Error.Unauthorized, null, ErrorType.UnauthorizedError);
            }
            var isUsersExist = await _userManager.FindByIdAsync(request.ReceiverAppUserId) is not null || await _userManager.Users.AnyAsync(s=>s.Id==request.SenderAppUserId&&s.Id==userId) is not null;
            if (!isUsersExist)
            {
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            }
            var chatMessage = new ChatMessage
            {
                SenderAppUserId = request.SenderAppUserId,
                ReceiverAppUserId = request.ReceiverAppUserId,
                Message = request.Message,
                SentAt = DateTime.UtcNow
            };
            await _unitOfWork.ChatMessageRepository.Create(chatMessage);
            await _unitOfWork.SaveChangesAsync();   
           return Result<Unit>.Success(Unit.Value,successReturnType:SuccessReturnType.Created);
        }
    }
}
