using SFC.Game.Application.Interfaces.Persistence.Repository.Invite.Data;
using SFC.Game.Domain.Common;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Repositories.Common.Data;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Invite.Data;
public class InviteDataRepository<TEntity, TEnum>(InviteDbContext context)
    : DataRepository<TEntity, InviteDbContext, TEnum>(context), IInviteDataRepository<TEntity, TEnum>
     where TEntity : EnumDataEntity<TEnum>
     where TEnum : struct
{ }