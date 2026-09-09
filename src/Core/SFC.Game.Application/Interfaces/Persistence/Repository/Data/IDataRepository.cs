using SFC.Game.Application.Interfaces.Persistence.Context;
using SFC.Game.Application.Interfaces.Persistence.Repository.Common.Data;
using SFC.Game.Domain.Common;

namespace SFC.Game.Application.Interfaces.Persistence.Repository.Data;

/// <summary>
/// Data related repository (Data service).
/// Enum based entities.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
/// <typeparam name="TEnum">Enum type.</typeparam>
public interface IDataRepository<TEntity, TEnum> : IDataRepository<TEntity, IDataDbContext, TEnum>
    where TEntity : EnumDataEntity<TEnum>
    where TEnum : struct
{ }