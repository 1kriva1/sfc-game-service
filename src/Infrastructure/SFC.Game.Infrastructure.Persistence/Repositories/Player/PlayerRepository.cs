using Microsoft.EntityFrameworkCore;

using SFC.Game.Application.Interfaces.Persistence.Repository.Player;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Extensions;
using SFC.Game.Infrastructure.Persistence.Repositories.Common;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Player;
public class PlayerRepository(PlayerDbContext context)
    : Repository<PlayerEntity, PlayerDbContext, long>(context), IPlayerRepository
{
    public override Task<PlayerEntity?> GetByIdAsync(long id)
    {
        return Context.Players
                    .Include(p => p.GeneralProfile)
                    .Include(p => p.FootballProfile)
                    .Include(p => p.Availability)
                    .Include(p => p.Availability.Days)
                    .Include(p => p.Points)
                    .Include(p => p.Tags)
                    .Include(p => p.Stats)
                    .Include(p => p.Photo)
                    .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PlayerEntity[]> AddRangeIfNotExistsAsync(params PlayerEntity[] entities)
    {
        await Context.Set<PlayerEntity>().AddRangeIfNotExistsAsync<PlayerEntity, long>(entities).ConfigureAwait(false);

        await Context.SaveChangesAsync().ConfigureAwait(false);

        return entities;
    }

    public Task<bool> AnyAsync(long id)
    {
        return Context.Players.AnyAsync(p => p.Id == id);
    }

    public Task<bool> AnyAsync(long id, Guid userId)
    {
        return Context.Players.AnyAsync(p => p.Id == id && p.UserId == userId);
    }
}