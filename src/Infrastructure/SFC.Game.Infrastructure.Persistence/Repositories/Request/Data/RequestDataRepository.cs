using SFC.Game.Application.Interfaces.Persistence.Repository.Request.Data;
using SFC.Game.Domain.Common;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Repositories.Common.Data;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Request.Data;
public class RequestDataRepository<TEntity, TEnum>(RequestDbContext context)
    : DataRepository<TEntity, RequestDbContext, TEnum>(context), IRequestDataRepository<TEntity, TEnum>
     where TEntity : EnumDataEntity<TEnum>
     where TEnum : struct
{ }