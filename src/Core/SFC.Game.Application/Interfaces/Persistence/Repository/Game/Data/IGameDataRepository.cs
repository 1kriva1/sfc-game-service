using SFC.Game.Application.Interfaces.Persistence.Context;
using SFC.Game.Application.Interfaces.Persistence.Repository.Common.Data;
using SFC.Game.Domain.Common;

namespace SFC.Game.Application.Interfaces.Persistence.Repository.Game.Data;
public interface IGameDataRepository<T, TEnum> : IDataRepository<T, IGameDbContext, TEnum>
    where T : EnumDataEntity<TEnum>
    where TEnum : struct
{
}