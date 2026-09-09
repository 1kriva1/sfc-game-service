using Microsoft.Extensions.DependencyInjection;

using SFC.Game.Application.Interfaces.Cache;
using SFC.Game.Application.Interfaces.Persistence.Repository.Request.Data;
using SFC.Game.Domain.Entities.Request.Data;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Request.Data.Cache;
public class RequestStatusCacheRepository(RequestStatusRepository repository, [FromKeyedServices(CacheInstance.Request)] ICache cache)
    : RequestDataCacheRepository<RequestStatus, RequestStatusEnum>(repository, cache), IRequestStatusRepository
{ }