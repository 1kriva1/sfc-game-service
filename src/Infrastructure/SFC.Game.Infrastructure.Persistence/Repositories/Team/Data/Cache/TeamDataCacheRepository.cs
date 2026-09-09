using Microsoft.Extensions.DependencyInjection;

using SFC.Game.Application.Interfaces.Cache;
using SFC.Game.Domain.Common;
using SFC.Game.Infrastructure.Persistence.Constants;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Repositories.Common.Data;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Team.Data.Cache;

public class TeamDataCacheRepository<TEntity, TEnum>(TeamDataRepository<TEntity, TEnum> repository, [FromKeyedServices(CacheInstance.Team)] ICache cache)
    : DataCacheRepository<TEntity, TeamDbContext, TEnum>(repository, cache)
     where TEntity : EnumDataEntity<TEnum>
     where TEnum : struct
{ }