using Microsoft.Extensions.DependencyInjection;

using SFC.Game.Application.Interfaces.Cache;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Data;
using SFC.Game.Domain.Entities.Game.Data;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Game.Data.Cache;
public class GameStatusCacheRepository(GameStatusRepository repository, [FromKeyedServices(CacheInstance.Game)] ICache cache)
    : GameDataCacheRepository<GameStatus, GameStatusEnum>(repository, cache), IGameStatusRepository
{ }