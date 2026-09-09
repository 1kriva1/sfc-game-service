using Microsoft.EntityFrameworkCore;

using SFC.Game.Application.Features.Common.Models.Find;
using SFC.Game.Application.Features.Common.Models.Find.Paging;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Extensions;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Game;
using SFC.Game.Infrastructure.Persistence.Repositories.Common;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Game.General;

public class GameRepository(GameDbContext context)
    : Repository<GameEntity, GameDbContext, long>(context), IGameRepository
{
    #region Public    

    public Task<GameEntity?> GetByIdAsync(long id, IEnumerable<string>? includes)
    {
        return Context.Games
                      .AsNoTracking()
                      .IncludeGame(includes)
                      .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<GameEntity>> GetByUserIdsAsync(IEnumerable<Guid> userIds)
    {
        return await Context.Games
                            .IncludeGame(GameIncludeOptions.Profile)
                            .Where(team => userIds.Contains(team.UserId))
                            .ToListAsync()
                            .ConfigureAwait(true);

    }

    public Task<PagedList<GameEntity>> FindAsync(FindParameters<GameEntity> parameters, IEnumerable<string>? includes)
    {
        return Context.Games
                      .AsNoTracking()
                      .IncludeGame(includes)
                      .AsQueryable()
                      .PaginateAsync(parameters);
    }

    public Task<bool> AnyAsync(long id)
    {
        return Context.Games.AnyAsync(u => u.Id == id);
    }

    public Task<bool> AnyAsync(long id, Guid userId)
    {
        return Context.Games.AnyAsync(u => u.Id == id && u.UserId == userId);
    }

    public async Task<GameEntity[]> AddRangeIfNotExistsAsync(params GameEntity[] entities)
    {
        await Context.Set<GameEntity>().AddRangeIfNotExistsAsync<GameEntity, long>(entities).ConfigureAwait(true);

        await Context.SaveChangesAsync().ConfigureAwait(true);

        return entities;
    }

    #endregion Public

    #region Ovveride

    public override Task<GameEntity?> GetByIdAsync(long id)
    {
        return Context.Games
                      .IncludeGame(GameIncludeOptions.Profile)
                      .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override Task<PagedList<GameEntity>> FindAsync(FindParameters<GameEntity> parameters)
    {
        return Context.Games
                      .IncludeGame(GameIncludeOptions.Profile)
                      .AsQueryable()
                      .PaginateAsync(parameters);
    }

    #endregion Ovveride
}