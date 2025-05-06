using EbuBridgeLmsSystem.Application.Features.AddressFeature.Queries.GetAllAddresses;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Queries.GetAddressById
{
    public class GetAddressByIdHandler : IRequestHandler<GetAddressByIdQuery, Result<AddressGetReturnDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAddressByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AddressGetReturnDto>> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var existedAddress=await _unitOfWork.AddressRepository.GetEntity(s=>s.Id==request.Id&&!s.IsDeleted, includes: new Func<IQueryable<Address>, IQueryable<Address>>[] {
                 query => query
            .Include(p => p.Country).Include(s=>s.City).Include(s=>s.AppUser) });
            if(existedAddress==null)
                return Result<AddressGetReturnDto>.Failure(Error.NotFound,null,ErrorType.NotFoundError);
            var newGetAddresReturnDto=new AddressGetReturnDto() { 
                Id = existedAddress.Id,
                Street=existedAddress.Street,   
                City=existedAddress.City.Name,
                Country=existedAddress.Country.Name,
                Region=existedAddress.Region,
                AppUserInAdress=new AppUserInAdress()
                {
                    Id = existedAddress.AppUser.Id,
                    UserName=existedAddress.AppUser.UserName,   
                }
            };
            return Result<AddressGetReturnDto>.Success(newGetAddresReturnDto, null);
        }
    }
}
