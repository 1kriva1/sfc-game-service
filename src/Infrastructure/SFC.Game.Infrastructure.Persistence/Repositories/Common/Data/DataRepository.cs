using Microsoft.EntityFrameworkCore;

using SFC.Game.Application.Interfaces.Persistence.Context;
using SFC.Game.Application.Interfaces.Persistence.Repository.Common.Data;
using SFC.Game.Domain.Common;
using SFC.Game.Infrastructure.Persistence.Extensions;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Common.Data;
public class DataRepository<TEntity, TContext, TEnum>(TContext context)
    : Repository<TEntity, TContext, TEnum>(context), IDataRepository<TEntity, TContext, TEnum>
     where TEntity : EnumDataEntity<TEnum>
     where TContext : DbContext, IDbContext
     where TEnum : struct
{
    public virtual Task<bool> AnyAsync(TEnum id)
    {
        return Context.Set<TEntity>().AnyAsync(u => u.Id.Equals(id));
    }

    public Task<TEntity[]> ResetAsync(IEnumerable<TEntity> entities)
    {
        Context.Clear<TEntity>();

        return AddRangeAsync(entities.ToArray());
    }
}