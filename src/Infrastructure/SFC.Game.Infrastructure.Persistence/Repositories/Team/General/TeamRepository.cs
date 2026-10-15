using Microsoft.EntityFrameworkCore;

using SFC.Game.Application.Interfaces.Persistence.Repository.Team.General;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Extensions;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Team;
using SFC.Game.Infrastructure.Persistence.Repositories.Common;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Team.General;
public class TeamRepository(TeamDbContext context)
    : Repository<TeamEntity, TeamDbContext, long>(context), ITeamRepository
{
    public override Task<TeamEntity?> GetByIdAsync(long id)
    {
        return Context.Teams
                    .IncludeTeam(TeamIncludeOptions.Profile)
                    .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<TeamEntity[]> AddRangeIfNotExistsAsync(params TeamEntity[] entities)
    {
        await Context.Set<TeamEntity>().AddRangeIfNotExistsAsync<TeamEntity, long>(entities).ConfigureAwait(false);

        await Context.SaveChangesAsync().ConfigureAwait(false);

        return entities;
    }

    public Task<bool> AnyAsync(long id)
    {
        return Context.Teams.AnyAsync(p => p.Id == id);
    }

    public Task<bool> AnyAsync(long id, Guid userId)
    {
        return Context.Teams.AnyAsync(p => p.Id == id && p.UserId == userId);
    }

    public Task<TeamEntity?> GetByIdAsync(long id, TeamIncludeOptions[] options)
    {
        return Context.Teams
                    .IncludeTeam(options)
                    .FirstOrDefaultAsync(p => p.Id == id);
    }
}