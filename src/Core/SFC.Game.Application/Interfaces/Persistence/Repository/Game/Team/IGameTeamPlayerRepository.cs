using SFC.Game.Application.Features.Common.Models.Find;
using SFC.Game.Application.Features.Common.Models.Find.Paging;
using SFC.Game.Application.Interfaces.Persistence.Context;
using SFC.Game.Application.Interfaces.Persistence.Repository.Common;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;

namespace SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;

public interface IGameTeamPlayerRepository : IRepository<GameTeamPlayer, IGameDbContext, long>
{
    Task<GameTeamPlayer?> GetByIdAsync(long gameId, long teamId, long playerId);

    Task<GameTeamPlayer?> GetByIdAsync(long id, long gameId, long teamId, long playerId);

    Task<GameTeamPlayer?> GetByIdAsync(long gameId, long teamId, long playerId, IEnumerable<string>? includes);

    Task<IReadOnlyList<GameTeamPlayer>> GetByIdsAsync(IEnumerable<long> gameIds, IEnumerable<long> teamIds, IEnumerable<long> playerIds);

    Task<IReadOnlyList<GameTeamPlayer>> ListAllAsync(long gameId, long teamId);

    Task<IReadOnlyList<GameTeamPlayer>> ListAllAsync(long gameId, long teamId, TeamPlayerStatusEnum status);

    Task<IReadOnlyList<GameTeamPlayer>> ListAllAsync(long gameId, long teamId, IEnumerable<string>? includes);

    Task<PagedList<GameTeamPlayer>> FindAsync(FindParameters<GameTeamPlayer> parameters, IEnumerable<string>? includes);

    Task<bool> AnyAsync(long gameId, long teamId, long playerId);

    Task<bool> AnyAsync(long gameId, long teamId, long playerId, TeamPlayerStatusEnum? status);

    Task<GameTeamPlayer[]> AddRangeIfNotExistsAsync(params GameTeamPlayer[] entities);
}