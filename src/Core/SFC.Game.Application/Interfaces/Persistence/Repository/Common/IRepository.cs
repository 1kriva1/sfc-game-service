using SFC.Game.Application.Features.Common.Models.Find;
using SFC.Game.Application.Features.Common.Models.Find.Paging;
using SFC.Game.Application.Interfaces.Persistence.Context;

namespace SFC.Game.Application.Interfaces.Persistence.Repository.Common;
public interface IRepository<TEntity, TContext, TId>
    where TEntity : class
    where TContext : IDbContext
{
    Task<TEntity?> GetByIdAsync(TId id);

    Task<IReadOnlyList<TEntity>> ListAllAsync();

    Task<PagedList<TEntity>> FindAsync(FindParameters<TEntity> parameters);

    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity[]> AddRangeAsync(params TEntity[] entities);

    Task UpdateAsync(TEntity entity);

    Task UpdateRangeAsync(params TEntity[] entities);

    Task DeleteAsync(TEntity entity);
}