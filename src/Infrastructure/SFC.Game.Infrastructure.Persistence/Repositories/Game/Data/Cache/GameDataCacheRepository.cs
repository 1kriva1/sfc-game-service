using Microsoft.Extensions.DependencyInjection;

using SFC.Game.Application.Interfaces.Cache;
using SFC.Game.Domain.Common;
using SFC.Game.Infrastructure.Persistence.Constants;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Repositories.Common.Data;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Game.Data.Cache;
public class GameDataCacheRepository<T, TEnum>(GameDataRepository<T, TEnum> repository, [FromKeyedServices(CacheInstance.Game)] ICache cache)
    : DataCacheRepository<T, GameDbContext, TEnum>(repository, cache)
     where T : EnumDataEntity<TEnum>
     where TEnum : struct
{
}