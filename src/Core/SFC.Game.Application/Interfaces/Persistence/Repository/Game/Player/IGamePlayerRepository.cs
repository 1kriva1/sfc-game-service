using SFC.Game.Application.Features.Common.Models.Find;
using SFC.Game.Application.Features.Common.Models.Find.Paging;
using SFC.Game.Application.Interfaces.Persistence.Context;
using SFC.Game.Application.Interfaces.Persistence.Repository.Common;
using SFC.Game.Domain.Entities.Game.Player;

namespace SFC.Game.Application.Interfaces.Persistence.Repository.Game.Player;

public interface IGamePlayerRepository : IRepository<GamePlayer, IGameDbContext, long>
{
    Task<GamePlayer?> GetByIdAsync(long gameId, long playerId);

    Task<GamePlayer?> GetByIdAsync(long id, long gameId, long playerId);

    Task<IReadOnlyList<GamePlayer>> GetByIdsAsync(IEnumerable<long> gameIds, IEnumerable<long> playerIds);

    Task<GamePlayer?> GetByIdAsync(long gameId, long playerId, IEnumerable<string>? includes);

    Task<PagedList<GamePlayer>> FindAsync(FindParameters<GamePlayer> parameters, IEnumerable<string>? includes);

    Task<IReadOnlyList<GamePlayer>> ListAllAsync(long gameId);

    Task<IReadOnlyList<GamePlayer>> ListAllAsync(long gameId, GamePlayerStatusEnum status);

    Task<bool> AnyAsync(long gameId, long playerId);

    Task<bool> AnyAsync(long gameId, long playerId, GamePlayerStatusEnum? status);

    Task<GamePlayer[]> AddRangeIfNotExistsAsync(params GamePlayer[] entities);
}