using SFC.Game.Application.Interfaces.Persistence.Repository.Data;
using SFC.Game.Domain.Common;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Repositories.Common.Data;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Data;
public class DataRepository<T, TEnum>(DataDbContext context)
    : DataRepository<T, DataDbContext, TEnum>(context), IDataRepository<T, TEnum>
     where T : EnumDataEntity<TEnum>
     where TEnum : struct
{ }