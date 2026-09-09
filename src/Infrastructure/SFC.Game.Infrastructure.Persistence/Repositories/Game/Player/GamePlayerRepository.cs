using Microsoft.EntityFrameworkCore;

using SFC.Game.Application.Features.Common.Models.Find;
using SFC.Game.Application.Features.Common.Models.Find.Paging;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Player;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Extensions;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Game;
using SFC.Game.Infrastructure.Persistence.Repositories.Common;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Game.Player;
public class GamePlayerRepository(GameDbContext context)
    : Repository<GamePlayer, GameDbContext, long>(context), IGamePlayerRepository
{
    public override Task<PagedList<GamePlayer>> FindAsync(FindParameters<GamePlayer> parameters)
    {
        return Context.GamePlayers
                      .IncludeGamePlayer(GamePlayerIncludeOptions.Player)
                      .AsQueryable()
                      .PaginateAsync(parameters);
    }

    public Task<GamePlayer?> GetByIdAsync(long gameId, long playerId)
    {
        return Context.GamePlayers
                      .IncludeGamePlayer(GamePlayerIncludeOptions.Player)
                      .FirstOrDefaultAsync(item => item.GameId == gameId && item.PlayerId == playerId);
    }

    public Task<GamePlayer?> GetByIdAsync(long id, long gameId, long playerId)
    {
        return Context.GamePlayers
                      .IncludeGamePlayer(GamePlayerIncludeOptions.Player)
                      .FirstOrDefaultAsync(item => item.Id == id && item.GameId == gameId && item.PlayerId == playerId);
    }

    public Task<GamePlayer?> GetByIdAsync(long gameId, long playerId, IEnumerable<string>? includes)
    {
        return Context.GamePlayers
                      .AsNoTracking()
                      .IncludeGamePlayer(includes, GamePlayerIncludeOptions.None)
                      .FirstOrDefaultAsync(item => item.GameId == gameId && item.PlayerId == playerId);
    }

    public async Task<IReadOnlyList<GamePlayer>> GetByIdsAsync(IEnumerable<long> gameIds, IEnumerable<long> playerIds)
    {
        return await Context.GamePlayers
                            .Where(gameTeam => gameIds.Contains(gameTeam.GameId) && playerIds.Contains(gameTeam.PlayerId))
                            .ToListAsync()
                            .ConfigureAwait(true);
    }

    public Task<PagedList<GamePlayer>> FindAsync(FindParameters<GamePlayer> parameters, IEnumerable<string>? includes)
    {
        return Context.GamePlayers
                      .AsNoTracking()
                      .IncludeGamePlayer(includes)
                      .AsQueryable()
                      .PaginateAsync(parameters);
    }

    public async Task<IReadOnlyList<GamePlayer>> ListAllAsync(long gameId)
    {
        return await Context.GamePlayers
                            .IncludeGamePlayer(GamePlayerIncludeOptions.Player)
                            .Where(gameTeam => gameTeam.GameId == gameId)
                            .ToListAsync()
                            .ConfigureAwait(true);
    }

    public async Task<IReadOnlyList<GamePlayer>> ListAllAsync(long gameId, GamePlayerStatusEnum status)
    {
        return await Context.GamePlayers
                            .IncludeGamePlayer(GamePlayerIncludeOptions.Player)
                            .Where(gameTeam => gameTeam.GameId == gameId && gameTeam.StatusId == status)
                            .ToListAsync()
                            .ConfigureAwait(true);
    }

    public Task<bool> AnyAsync(long gameId, long playerId)
    {
        return Context.GamePlayers.AnyAsync(item => item.GameId == gameId && item.PlayerId == playerId);
    }

    public Task<bool> AnyAsync(long gameId, long playerId, GamePlayerStatusEnum? status)
    {
        return status.HasValue
            ? Context.GamePlayers.AnyAsync(item => item.GameId == gameId && item.PlayerId == playerId && item.StatusId == status)
            : Context.GamePlayers.AnyAsync(item => item.GameId == gameId && item.PlayerId == playerId);
    }

    public async Task<GamePlayer[]> AddRangeIfNotExistsAsync(params GamePlayer[] entities)
    {
        await Context.Set<GamePlayer>().AddRangeIfNotExistsAsync<GamePlayer, long>(entities).ConfigureAwait(true);

        await Context.SaveChangesAsync().ConfigureAwait(true);

        return entities;
    }
}