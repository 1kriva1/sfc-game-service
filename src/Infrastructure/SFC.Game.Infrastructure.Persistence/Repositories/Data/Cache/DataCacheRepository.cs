using Microsoft.Extensions.DependencyInjection;

using SFC.Game.Application.Interfaces.Cache;
using SFC.Game.Domain.Common;
using SFC.Game.Infrastructure.Persistence.Constants;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Repositories.Common.Data;
using SFC.Game.Infrastructure.Persistence.Repositories.Data;

namespace SFC.Scheme.Infrastructure.Persistence.Repositories.Data.Cache;

public class DataCacheRepository<TEntity, TEnum>(DataRepository<TEntity, TEnum> repository, [FromKeyedServices(CacheInstance.Data)] ICache cache)
    : DataCacheRepository<TEntity, DataDbContext, TEnum>(repository, cache)
     where TEntity : EnumDataEntity<TEnum>
     where TEnum : struct
{ }