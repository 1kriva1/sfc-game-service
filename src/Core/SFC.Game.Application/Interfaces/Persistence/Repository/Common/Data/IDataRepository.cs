using SFC.Game.Application.Interfaces.Persistence.Context;
using SFC.Game.Domain.Common;

namespace SFC.Game.Application.Interfaces.Persistence.Repository.Common.Data;
public interface IDataRepository<TEntity, TContext, TEnum> : IRepository<TEntity, TContext, TEnum>
    where TEntity : EnumDataEntity<TEnum>
    where TContext : IDbContext
    where TEnum : struct
{
    /// <summary>
    /// Check if data related entity exist in database.
    /// </summary>
    /// <param name="id">Entity identifier.</param>
    /// <returns>True if exist, othrwise false.</returns>
    Task<bool> AnyAsync(TEnum id);

    /// <summary>
    /// Reset all data related enteties.
    /// </summary>
    /// <param name="entities">Data related enteties.</param>
    /// <returns>Reseted data related enteties.</returns>
    Task<TEntity[]> ResetAsync(IEnumerable<TEntity> entities);
}