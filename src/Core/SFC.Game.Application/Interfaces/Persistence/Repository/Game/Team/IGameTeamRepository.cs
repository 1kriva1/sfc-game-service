using SFC.Game.Application.Features.Common.Models.Find;
using SFC.Game.Application.Features.Common.Models.Find.Paging;
using SFC.Game.Application.Interfaces.Persistence.Context;
using SFC.Game.Application.Interfaces.Persistence.Repository.Common;
using SFC.Game.Domain.Entities.Game.Team.General;

namespace SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;

public interface IGameTeamRepository : IRepository<GameTeam, IGameDbContext, long>
{
    Task<GameTeam?> GetByIdAsync(long gameId, long teamId);

    Task<GameTeam?> GetByIdAsync(long id, long gameId, long teamId);

    Task<IReadOnlyList<GameTeam>> GetByIdsAsync(IEnumerable<long> gameIds, IEnumerable<long> teamIds);

    Task<GameTeam?> GetByIdAsync(long gameId, long teamId, IEnumerable<string>? includes);

    Task<PagedList<GameTeam>> FindAsync(FindParameters<GameTeam> parameters, IEnumerable<string>? includes);

    Task<IReadOnlyList<GameTeam>> ListAllAsync(long gameId);

    Task<IReadOnlyList<GameTeam>> ListAllAsync(long gameId, GameTeamStatusEnum status);

    Task<IReadOnlyList<GameTeam>> ListAllAsync(long gameId, IEnumerable<string>? includes);

    Task<bool> AnyAsync(long gameId, long teamId);

    Task<bool> AnyAsync(long gameId, long teamId, GameTeamStatusEnum? status);

    Task<GameTeam[]> AddRangeIfNotExistsAsync(params GameTeam[] entities);
}