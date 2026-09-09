using Microsoft.Extensions.DependencyInjection;

using SFC.Game.Application.Interfaces.Cache;
using SFC.Game.Domain.Common;
using SFC.Game.Infrastructure.Persistence.Constants;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Repositories.Common.Data;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Request.Data.Cache;
public class RequestDataCacheRepository<TEntity, TEnum>(RequestDataRepository<TEntity, TEnum> repository, [FromKeyedServices(CacheInstance.Request)] ICache cache)
    : DataRelatedCacheRepository<TEntity, RequestDbContext, TEnum>(repository, cache)
     where TEntity : EnumDataEntity<TEnum>
     where TEnum : struct
{ }