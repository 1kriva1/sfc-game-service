using Microsoft.EntityFrameworkCore;

using SFC.Game.Application.Features.Common.Models.Find;
using SFC.Game.Application.Features.Common.Models.Find.Paging;
using SFC.Game.Application.Interfaces.Persistence.Context;
using SFC.Game.Application.Interfaces.Persistence.Repository.Common;
using SFC.Game.Infrastructure.Persistence.Extensions;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Common;

public class Repository<TEntity, TContext, TId>(TContext context) : IRepository<TEntity, TContext, TId>
    where TEntity : class
    where TContext : DbContext, IDbContext
{
    protected TContext Context { get; } = context;

    public virtual async Task<TEntity?> GetByIdAsync(TId id)
    {
        TEntity? entity = await Context.Set<TEntity>()
                                       .FindAsync(id)
                                       .ConfigureAwait(false);
        return entity;
    }

    public virtual Task<PagedList<TEntity>> FindAsync(FindParameters<TEntity> parameters)
    {
        return Context.Set<TEntity>()
                      .AsQueryable<TEntity>()
                      .PaginateAsync(parameters);
    }

    public virtual async Task<IReadOnlyList<TEntity>> ListAllAsync()
    {
        return await Context.Set<TEntity>()
                            .ToListAsync()
                            .ConfigureAwait(false);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>()
                     .AddAsync(entity)
                     .ConfigureAwait(false);

        await Context.SaveChangesAsync()
                     .ConfigureAwait(false);

        return entity;
    }

    public virtual async Task<TEntity[]> AddRangeAsync(params TEntity[] entities)
    {
        await Context.Set<TEntity>()
                     .AddRangeAsync(entities)
                     .ConfigureAwait(false);

        await Context.SaveChangesAsync()
                     .ConfigureAwait(false);

        return entities;
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;

        return Context.SaveChangesAsync();
    }

    public virtual async Task UpdateRangeAsync(params TEntity[] entities)
    {
        Context.Set<TEntity>()
               .UpdateRange(entities);

        await Context.SaveChangesAsync()
                     .ConfigureAwait(false);
    }

    public Task DeleteAsync(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);

        return Context.SaveChangesAsync();
    }
}