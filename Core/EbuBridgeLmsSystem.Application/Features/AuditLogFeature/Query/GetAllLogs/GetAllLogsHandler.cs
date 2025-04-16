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
            var auditLogQuery = await _unitOfWork.AuditLogRepository.GetQuery(s => (string.IsNullOrWhiteSpace(tableName) ||
                             EF.Functions.Like(s.TableName, $"%{tableName}%")) &&
                            (string.IsNullOrWhiteSpace(action) ||
                             EF.Functions.Like(s.Action, $"%{action}%")) &&
                            (string.IsNullOrWhiteSpace(userId) ||
                             EF.Functions.Like(s.UserId, $"%{userId}%"))&&!s.IsDeleted, true);
            
            auditLogQuery = auditLogQuery.OrderByDescending(s => s.CreatedTime);
            var paginationResult = await _unitOfWork.AuditLogRepository.GetPaginatedResultAsync(request.Cursor, request.Limit);
            var mappedResult = new PaginatedResult<AuditLogListItemDto>
            {
                Data = paginationResult.Data.Select(auditLog => new AuditLogListItemDto
                {
                    TableName = auditLog.TableName,
                    Action = auditLog.Action,
                    UserId = auditLog.UserId,
                    UserName = auditLog.UserName,
                    Timestamp = auditLog.Timestamp,
                    Changes = auditLog.Changes,
                    ClientIpAddress = auditLog.ClientIpAddress
                }).AsEnumerable(),
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

