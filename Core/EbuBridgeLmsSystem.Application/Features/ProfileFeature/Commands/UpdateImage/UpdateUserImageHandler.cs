using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Helpers.Enums;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.ProfileFeature.Commands.UpdateImage
{
    public class UpdateUserImageHandler : IRequestHandler<UpdateUserImageCommand, Result<Unit>>
    {
        private readonly IPhotoOrVideoService _photoOrVideoService;
        private     readonly UserManager<AppUser> _userManager;
        private readonly IAppUserResolver _userResolver;
        private   readonly ILogger<UpdateUserImageHandler> _logger;
        public UpdateUserImageHandler(UserManager<AppUser> userManager, IPhotoOrVideoService photoOrVideoService, ILogger<UpdateUserImageHandler> logger)
        {
            _userManager = userManager;
            _photoOrVideoService = photoOrVideoService;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(UpdateUserImageCommand request, CancellationToken cancellationToken)
        {
            var currentUserInAuth =await _userResolver.GetCurrentUserAsync();
            if (currentUserInAuth is null)
            {
                return Result<Unit>.Failure(Error.Custom("Non-Authorized", "User in system doesnt exist"),null,ErrorType.UnauthorizedError);
            }
            var isUserExist=await _userManager.Users.AnyAsync(s=>s.Id == currentUserInAuth.Id);
            if(!isUserExist)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            if (string.IsNullOrWhiteSpace(currentUserInAuth.Image))
            {
                try
                {
                    await _photoOrVideoService.DeleteMediaAsync(currentUserInAuth.Image, FileResourceType.Image);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred during user image delete");
                    return Result<Unit>.Failure(Error.InternalServerError, null, ErrorType.SystemError);
                }
            }
            try
            {
           var result=await _photoOrVideoService.UploadMediaAsync(request.FormFile, false);
                if(result is null||result is not string)
                    return Result<Unit>.Failure(Error.SystemError, null, ErrorType.SystemError);
                currentUserInAuth.Image = result;
                await _userManager.UpdateAsync(currentUserInAuth);

            } catch (Exception ex) {
                _logger.LogError(ex, "Error occurred during user image update");
                return Result<Unit>.Failure(Error.InternalServerError, null, ErrorType.SystemError);
            }
            return Result<Unit>.Success(Unit.Value, SuccessReturnType.NoContent);
        }
    }
}
