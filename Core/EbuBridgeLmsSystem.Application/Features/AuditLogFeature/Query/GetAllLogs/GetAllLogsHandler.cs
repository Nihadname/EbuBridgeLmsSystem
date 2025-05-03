using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EbuBridgeLmsSystem.Application.Features.AuditLogFeature.Query.GetAllLogs
{
    public class GetAllLogsHandler : IRequestHandler<GetAllLogsQuery, Result<PaginatedResult<AuditLogListItemDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _cache;
        private readonly ILogger<GetAllLogsHandler> _logger;
        public GetAllLogsHandler(IDistributedCache cache, IUnitOfWork unitOfWork, ILogger<GetAllLogsHandler> logger)
        {
            _cache = cache;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<PaginatedResult<AuditLogListItemDto>>> Handle(GetAllLogsQuery request, CancellationToken cancellationToken)
        {
            string tableName = request.TableNameSearchQuery;
            string action = request.ActionSearchQuery;
            string userId = request.UserId;
            string cacheKey = $"cities_{request.Cursor}_{request.Limit}_{tableName}_{action}_{userId}";
            var cachedData = await _cache.GetStringAsync(cacheKey,cancellationToken);
            if (!string.IsNullOrWhiteSpace(cachedData))
            {
                var cachedResult = JsonSerializer.Deserialize<PaginatedResult<AuditLogListItemDto>>(cachedData);
                if (cachedResult != null)
                {
                    return Result<PaginatedResult<AuditLogListItemDto>>.Success(cachedResult, null);
                }
            }
            var auditLogQuery =  _unitOfWork.AuditLogRepository.GetSelected(s=> new AuditLogListItemDto()
            {
                Id = s.Id,
                TableName = s.TableName,
                Action = s.Action,
                UserId = s.UserId,
                UserName = s.UserName,
                Changes = s.Changes,
                ClientIpAddress = s.ClientIpAddress,
            }, s => (string.IsNullOrWhiteSpace(tableName) ||
                             EF.Functions.Like(s.TableName, $"%{tableName}%")) &&
                            (string.IsNullOrWhiteSpace(action) ||
                             EF.Functions.Like(s.Action, $"%{action}%")) &&
                            (string.IsNullOrWhiteSpace(userId) ||
                             EF.Functions.Like(s.UserId, $"%{userId}%")));
            
            auditLogQuery = auditLogQuery.OrderByDescending(s => s.CreatedTime);
            var paginationResult = await _unitOfWork.AuditLogRepository.GetPaginatedResultAsync(query: auditLogQuery,
    cursor: request.Cursor,
    limit: request.Limit,
    sortKey: s => s.Id);
            var mappedResult = new PaginatedResult<AuditLogListItemDto>
            {
                Data = paginationResult.Data,
                NextCursor = paginationResult.NextCursor
            };
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                };

                try
                {
                    await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(mappedResult), cacheOptions,cancellationToken);
                }
                catch (Exception ex)
                {
                _logger.LogError($"Error caching audit logs with key {cacheKey}: {ex.Message}");
                return Result<PaginatedResult<AuditLogListItemDto>>.Failure(Error.SystemError, null, ErrorType.SystemError);
                }
                return Result<PaginatedResult<AuditLogListItemDto>>.Success(mappedResult, null);
            }
            
        }
    }

