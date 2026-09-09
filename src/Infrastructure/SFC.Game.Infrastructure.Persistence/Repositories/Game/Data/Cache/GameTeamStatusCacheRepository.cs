using Microsoft.Extensions.DependencyInjection;

using SFC.Game.Application.Interfaces.Cache;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Data;
using SFC.Game.Domain.Entities.Game.Data;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Game.Data.Cache;
public class GameTeamStatusCacheRepository(GameTeamStatusRepository repository, [FromKeyedServices(CacheInstance.Game)] ICache cache)
    : GameDataCacheRepository<GameTeamStatus, GameTeamStatusEnum>(repository, cache), IGameTeamStatusRepository
{ }