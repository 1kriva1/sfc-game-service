using Microsoft.Extensions.DependencyInjection;

using SFC.Game.Application.Interfaces.Cache;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Data;
using SFC.Game.Domain.Entities.Game.Data;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Game.Data.Cache;
public class GamePlayerStatusCacheRepository(GamePlayerStatusRepository repository, [FromKeyedServices(CacheInstance.Game)] ICache cache)
    : GameDataCacheRepository<GamePlayerStatus, GamePlayerStatusEnum>(repository, cache), IGamePlayerStatusRepository
{ }