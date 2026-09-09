using Microsoft.EntityFrameworkCore;

using SFC.Game.Application.Features.Common.Models.Find;
using SFC.Game.Application.Features.Common.Models.Find.Paging;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Extensions;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Game;
using SFC.Game.Infrastructure.Persistence.Repositories.Common;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Game.Team.General;
public class GameTeamRepository(GameDbContext context)
    : Repository<GameTeam, GameDbContext, long>(context), IGameTeamRepository
{
    public override Task<PagedList<GameTeam>> FindAsync(FindParameters<GameTeam> parameters)
    {
        return Context.GameTeams
                      .IncludeGameTeam(GameTeamIncludeOptions.TeamProfile, GameTeamIncludeOptions.GameTeamPlayersWithPlayerProfile)
                      .AsQueryable()
                      .PaginateAsync(parameters);
    }

    public Task<GameTeam?> GetByIdAsync(long gameId, long teamId)
    {
        return Context.GameTeams
                      .IncludeGameTeam(GameTeamIncludeOptions.TeamProfile, GameTeamIncludeOptions.GameTeamPlayersWithPlayerProfile)
                      .FirstOrDefaultAsync(item => item.GameId == gameId && item.TeamId == teamId);
    }

    public Task<GameTeam?> GetByIdAsync(long id, long gameId, long teamId)
    {
        return Context.GameTeams
                      .IncludeGameTeam(GameTeamIncludeOptions.TeamProfile, GameTeamIncludeOptions.GameTeamPlayersWithPlayerProfile)
                      .FirstOrDefaultAsync(item => item.Id == id && item.GameId == gameId && item.Team.Id == teamId);
    }

    public Task<GameTeam?> GetByIdAsync(long gameId, long teamId, IEnumerable<string>? includes)
    {
        return Context.GameTeams
                      .AsNoTracking()
                      .IncludeGameTeam(includes)
                      .FirstOrDefaultAsync(item => item.GameId == gameId && item.TeamId == teamId);
    }

    public async Task<IReadOnlyList<GameTeam>> GetByIdsAsync(IEnumerable<long> gameIds, IEnumerable<long> teamIds)
    {
        return await Context.GameTeams
                            .Where(gameTeam => gameIds.Contains(gameTeam.GameId) && teamIds.Contains(gameTeam.TeamId))
                            .ToListAsync()
                            .ConfigureAwait(true);
    }

    public async Task<IReadOnlyList<GameTeam>> ListAllAsync(long gameId)
    {
        return await Context.GameTeams
                            .IncludeGameTeam(GameTeamIncludeOptions.TeamProfile, GameTeamIncludeOptions.GameTeamPlayersWithPlayerProfile)
                            .Where(gameTeam => gameTeam.GameId == gameId)
                            .ToListAsync()
                            .ConfigureAwait(true);
    }

    public async Task<IReadOnlyList<GameTeam>> ListAllAsync(long gameId, IEnumerable<string>? includes)
    {
        return await Context.GameTeams
                            .AsNoTracking()
                            .IncludeGameTeam(includes)
                            .Where(gameTeam => gameTeam.GameId == gameId)
                            .ToListAsync()
                            .ConfigureAwait(true);
    }

    public async Task<IReadOnlyList<GameTeam>> ListAllAsync(long gameId, GameTeamStatusEnum status)
    {
        return await Context.GameTeams
                            .IncludeGameTeam(GameTeamIncludeOptions.TeamProfile, GameTeamIncludeOptions.GameTeamPlayersWithPlayerProfile)
                            .Where(gameTeam => gameTeam.GameId == gameId && gameTeam.StatusId == status)
                            .ToListAsync()
                            .ConfigureAwait(true);
    }

    public Task<PagedList<GameTeam>> FindAsync(FindParameters<GameTeam> parameters, IEnumerable<string>? includes)
    {
        return Context.GameTeams
                      .AsNoTracking()
                      .IncludeGameTeam(includes)
                      .AsQueryable()
                      .PaginateAsync(parameters);
    }

    public Task<bool> AnyAsync(long gameId, long teamId)
    {
        return Context.GameTeams.AnyAsync(item => item.GameId == gameId && item.Team.Id == teamId);
    }

    public Task<bool> AnyAsync(long gameId, long teamId, GameTeamStatusEnum? status)
    {
        return status.HasValue
            ? Context.GameTeams.AnyAsync(item => item.GameId == gameId && item.Team.Id == teamId && item.StatusId == status)
            : Context.GameTeams.AnyAsync(item => item.GameId == gameId && item.Team.Id == teamId);
    }

    public async Task<GameTeam[]> AddRangeIfNotExistsAsync(params GameTeam[] entities)
    {
        await Context.Set<GameTeam>().AddRangeIfNotExistsAsync<GameTeam, long>(entities).ConfigureAwait(true);

        await Context.SaveChangesAsync().ConfigureAwait(true);

        return entities;
    }
}