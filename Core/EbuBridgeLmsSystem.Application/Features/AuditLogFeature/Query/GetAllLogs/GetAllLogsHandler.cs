using EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetAllCities;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AuditLogFeature.Query.GetAllLogs
{
    public class GetAllLogsHandler : IRequestHandler<GetAllLogsQuery, Result<PaginatedResult<AuditLogListItemDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _cache;

        public GetAllLogsHandler(IDistributedCache cache, IUnitOfWork unitOfWork)
        {
            _cache = cache;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PaginatedResult<AuditLogListItemDto>>> Handle(GetAllLogsQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"cities_{request.Cursor}_{request.Limit}_{request.TableNameSearchQuery?.ToLower()}_{request.ActionSearchQuery?.ToLower()}_{request.UserId?.ToLower()}";
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrWhiteSpace(cachedData))
            {
                var cachedResult = JsonSerializer.Deserialize<PaginatedResult<AuditLogListItemDto>>(cachedData);
                return Result<PaginatedResult<AuditLogListItemDto>>.Success(cachedResult);
            }
            var auditLogQuery = await _unitOfWork.AuditLogRepository.GetQuery(null, true);
            if (!string.IsNullOrWhiteSpace(request.TableNameSearchQuery))
            {
                auditLogQuery = auditLogQuery.Where(s => s.TableName.ToLower().Contains(request.TableNameSearchQuery.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(request.ActionSearchQuery))
            {
                auditLogQuery = auditLogQuery.Where(s => s.Action.ToLower().Contains(request.ActionSearchQuery.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(request.UserId))
            {
                auditLogQuery = auditLogQuery.Where(s => s.UserId.ToLower().Contains(request.UserId.ToLower()));
            }
            auditLogQuery = auditLogQuery.OrderByDescending(s => s.CreatedTime);
            var paginationResult = await _unitOfWork.AuditLogRepository.GetPaginatedResultAsync(request.Cursor, request.Limit);
            var mappedResult = new PaginatedResult<AuditLogListItemDto>
            {
                Data = paginationResult.Data.Select(auditLog => new AuditLogListItemDto
                {
                   TableName= auditLog.TableName,
                   Action= auditLog.Action,
                   UserId= auditLog.UserId,
                   Timestamp= auditLog.Timestamp,
                   Changes= auditLog.Changes,
                    ClientIpAddress = auditLog.ClientIpAddress
                }).ToList(),
                NextCursor = paginationResult.NextCursor
            };
            throw new NotImplementedException();
        }
    }
}
