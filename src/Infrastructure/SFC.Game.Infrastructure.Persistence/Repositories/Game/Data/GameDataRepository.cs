using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Data;
using SFC.Game.Domain.Common;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Repositories.Common.Data;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Game.Data;
public class GameDataRepository<T, TEnum>(GameDbContext context)
     : DataRepository<T, GameDbContext, TEnum>(context), IGameDataRepository<T, TEnum>
     where T : EnumDataEntity<TEnum>
     where TEnum : struct
{
}