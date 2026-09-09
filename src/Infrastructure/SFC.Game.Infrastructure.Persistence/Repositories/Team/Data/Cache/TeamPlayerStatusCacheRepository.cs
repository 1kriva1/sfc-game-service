using Microsoft.Extensions.DependencyInjection;

using SFC.Game.Application.Interfaces.Cache;
using SFC.Game.Application.Interfaces.Persistence.Repository.Team.Data;
using SFC.Game.Domain.Entities.Team.Data;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Team.Data.Cache;

public class TeamPlayerStatusCacheRepository(TeamPlayerStatusRepository repository, [FromKeyedServices(CacheInstance.Team)] ICache cache)
    : TeamDataCacheRepository<TeamPlayerStatus, TeamPlayerStatusEnum>(repository, cache), ITeamPlayerStatusRepository
{ }