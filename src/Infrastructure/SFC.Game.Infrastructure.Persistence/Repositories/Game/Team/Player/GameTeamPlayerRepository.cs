using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using SFC.Game.Application.Features.Common.Models.Find;
using SFC.Game.Application.Features.Common.Models.Find.Paging;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team.Player;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Entities.Team.Player;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Extensions;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Game;
using SFC.Game.Infrastructure.Persistence.Repositories.Common;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Game.Team.Player;

public class GameTeamPlayerRepository(GameDbContext context)
    : Repository<GameTeamPlayer, GameDbContext, long>(context), IGameTeamPlayerRepository
{
    public override Task<PagedList<GameTeamPlayer>> FindAsync(FindParameters<GameTeamPlayer> parameters)
    {
        return Context.GameTeamPlayers
                      .IncludeGameTeamPlayer(GameTeamPlayerIncludeOptions.GameTeam, GameTeamPlayerIncludeOptions.GamePlayerWithPlayerProfile, GameTeamPlayerIncludeOptions.TeamPlayerWithPlayerProfile)
                      .AsQueryable()
                      .PaginateAsync(parameters);
    }

    public Task<GameTeamPlayer?> GetByIdAsync(long gameId, long teamId, long playerId)
    {
        return Context.GameTeamPlayers
                      .IncludeGameTeamPlayer(GameTeamPlayerIncludeOptions.GameTeam, GameTeamPlayerIncludeOptions.GamePlayerWithPlayerProfile, GameTeamPlayerIncludeOptions.TeamPlayerWithPlayerProfile)
                      .FirstOrDefaultAsync(item =>
                        item.GameTeam.GameId == gameId &&
                        item.GameTeam.TeamId == teamId &&
                        ((item.GamePlayer != null && item.GamePlayer.PlayerId == playerId) || (item.TeamPlayer != null && item.TeamPlayer.PlayerId == playerId)));
    }

    public Task<GameTeamPlayer?> GetByIdAsync(long id, long gameId, long teamId, long playerId)
    {
        return Context.GameTeamPlayers
                      .IncludeGameTeamPlayer(GameTeamPlayerIncludeOptions.GameTeam, GameTeamPlayerIncludeOptions.GamePlayerWithPlayerProfile, GameTeamPlayerIncludeOptions.TeamPlayerWithPlayerProfile)
                      .FirstOrDefaultAsync(item =>
                      item.Id == id &&
                      item.GameTeam.GameId == gameId &&
                      item.GameTeam.TeamId == teamId &&
                      ((item.GamePlayer != null && item.GamePlayer.PlayerId == playerId) || (item.TeamPlayer != null && item.TeamPlayer.PlayerId == playerId)));
    }

    public Task<GameTeamPlayer?> GetByIdAsync(long gameId, long teamId, long playerId, IEnumerable<string>? includes)
    {
        return Context.GameTeamPlayers
                      .IncludeGameTeamPlayer(GameTeamPlayerIncludeOptions.GamePlayerWithPlayerProfile, GameTeamPlayerIncludeOptions.TeamPlayerWithPlayerProfile)
                      .IncludeGameTeamPlayer(includes)
                      .FirstOrDefaultAsync(item =>
                        item.GameTeam.GameId == gameId &&
                        item.GameTeam.TeamId == teamId &&
                        ((item.GamePlayer != null && item.GamePlayer.PlayerId == playerId) || (item.TeamPlayer != null && item.TeamPlayer.PlayerId == playerId)));
    }

    public Task<GameTeamPlayer?> GetByIdAsync(long gameId, long teamId, long? gamePlayerId, long? teamPlayerId, IEnumerable<string>? includes)
    {
        return Context.GameTeamPlayers
                      .AsNoTracking()
                      .IncludeGameTeamPlayer(includes)
                      .FirstOrDefaultAsync(item =>
                      item.GameTeam.GameId == gameId &&
                      item.GameTeam.TeamId == teamId &&
                      (item.GamePlayerId == gamePlayerId || item.TeamPlayerId == teamPlayerId));
    }

    public Task<GameTeamPlayer?> GetByIdAsync(long gameId, long teamId, long? gamePlayerId, long? teamPlayerId, GameTeamPlayerIncludeOptions[] options)
    {
        return Context.GameTeamPlayers
                      .AsNoTracking()
                      .IncludeGameTeamPlayer(options)
                      .FirstOrDefaultAsync(item =>
                      item.GameTeam.GameId == gameId &&
                      item.GameTeam.TeamId == teamId &&
                      (item.GamePlayerId == gamePlayerId || item.TeamPlayerId == teamPlayerId));
    }

    public async Task<IReadOnlyList<GameTeamPlayer>> GetByIdsAsync(
        IEnumerable<long> gameIds,
        IEnumerable<long> teamIds,
        IEnumerable<long> playerIds)
    {
        return await Context.GameTeamPlayers
                            .IncludeGameTeamPlayer(GameTeamPlayerIncludeOptions.GamePlayerWithPlayer, GameTeamPlayerIncludeOptions.TeamPlayerWithPlayer)
                            .Where(gameTeamPlayer =>
                            gameIds.Contains(gameTeamPlayer.GameTeam.GameId) &&
                            teamIds.Contains(gameTeamPlayer.GameTeam.TeamId) &&
                            (gameTeamPlayer.GamePlayer != null && playerIds.Contains(gameTeamPlayer.GamePlayer.PlayerId) ||
                                (gameTeamPlayer.TeamPlayer != null && playerIds.Contains(gameTeamPlayer.TeamPlayer.PlayerId))))
                            .ToListAsync()
                            .ConfigureAwait(true);
    }

    public async Task<IReadOnlyList<GameTeamPlayer>> GetByIdsAsync(
        IEnumerable<long> gameIds,
        IEnumerable<long> teamIds,
        IEnumerable<long?> gamePlayerIds,
        IEnumerable<long?> teamPlayerIds,
        GameTeamPlayerIncludeOptions[] options)
    {
        return await Context.GameTeamPlayers
                            .IncludeGameTeamPlayer(options)
                            .Where(gameTeamPlayer =>
                            gameIds.Contains(gameTeamPlayer.GameTeam.GameId) &&
                            teamIds.Contains(gameTeamPlayer.GameTeam.TeamId) &&
                            (gameTeamPlayer.GamePlayerId.HasValue && gamePlayerIds.Contains(gameTeamPlayer.GamePlayerId) || (gameTeamPlayer.TeamPlayerId.HasValue && teamPlayerIds.Contains(gameTeamPlayer.TeamPlayerId))))
                            .ToListAsync()
                            .ConfigureAwait(true);
    }

    public async Task<IReadOnlyList<GameTeamPlayer>> ListAllAsync(long gameId, long teamId)
    {
        return await Context.GameTeamPlayers
                            .IncludeGameTeamPlayer(GameTeamPlayerIncludeOptions.GameTeam, GameTeamPlayerIncludeOptions.GamePlayerWithPlayerProfile, GameTeamPlayerIncludeOptions.TeamPlayerWithPlayerProfile)
                            .Where(gameTeamPlayer => gameTeamPlayer.GameTeam.GameId == gameId && gameTeamPlayer.GameTeam.TeamId == teamId)
                            .ToListAsync()
                            .ConfigureAwait(true);
    }

    public async Task<IReadOnlyList<GameTeamPlayer>> ListAllAsync(long gameId, long teamId, IEnumerable<string>? includes)
    {
        return await Context.GameTeamPlayers
                            .AsNoTracking()
                            .IncludeGameTeamPlayer(includes)
                            .Where(gameTeamPlayer => gameTeamPlayer.GameTeam.GameId == gameId && gameTeamPlayer.GameTeam.TeamId == teamId)
                            .ToListAsync()
                            .ConfigureAwait(true);
    }

    public Task<PagedList<GameTeamPlayer>> FindAsync(FindParameters<GameTeamPlayer> parameters, IEnumerable<string>? includes)
    {
        return Context.GameTeamPlayers
                      .AsNoTracking()
                      .IncludeGameTeamPlayer(includes)
                      .AsQueryable()
                      .PaginateAsync(parameters);
    }

    public Task<bool> AnyAsync(long gameId, long teamId, long playerId)
    {
        return Context.GameTeamPlayers.AnyAsync(item => item.GameTeam.GameId == gameId && item.GameTeam.TeamId == teamId && (item.GamePlayerId == playerId || item.TeamPlayerId == playerId));
    }

    public async Task<GameTeamPlayer[]> AddRangeIfNotExistsAsync(params GameTeamPlayer[] entities)
    {
        await Context.Set<GameTeamPlayer>().AddRangeIfNotExistsAsync<GameTeamPlayer, long>(entities).ConfigureAwait(true);

        await Context.SaveChangesAsync().ConfigureAwait(true);

        return entities;
    }
}