using SFC.Game.Domain.Common;

namespace SFC.Game.Application.Interfaces.Reference;

/// <summary>
/// Contract to get entities from other services.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
/// <typeparam name="TID">Entity identifier type.</typeparam>
/// <typeparam name="TDto">Entity related DTO type.</typeparam>
public interface IReference<TEntity, TId, TDto>
    where TEntity : BaseEntity<TId>
{
    Task<TEntity?> GetAsync(TId id, CancellationToken cancellationToken = default);
}