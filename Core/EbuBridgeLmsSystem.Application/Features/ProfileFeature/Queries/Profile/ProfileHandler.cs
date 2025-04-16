using AutoMapper;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.ProfileFeature.Queries.Profile
{
    public class ProfileHandler : IRequestHandler<ProfileQuery, Result<UserGetDto>>
    {
        private readonly IAppUserResolver _userResolver;
        private readonly IMapper _mapper;
        public ProfileHandler(IAppUserResolver userResolver, IMapper mapper)
        {
            _userResolver = userResolver;
            _mapper = mapper;
        }

        public async Task<Result<UserGetDto>> Handle(ProfileQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userResolver.GetCurrentUserAsync();
            if(currentUser == null)
                return Result<UserGetDto>.Failure(Error.Unauthorized,null, ErrorType.UnauthorizedError);
            var mappedUser = _mapper.Map<UserGetDto>(currentUser);
            return Result<UserGetDto>.Success(mappedUser, null);

        }
    }
}
