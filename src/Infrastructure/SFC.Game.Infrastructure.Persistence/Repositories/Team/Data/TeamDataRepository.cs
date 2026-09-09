using SFC.Game.Application.Interfaces.Persistence.Repository.Team.Data;
using SFC.Game.Domain.Common;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Repositories.Common.Data;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Team.Data;
public class TeamDataRepository<TEntity, TEnum>(TeamDbContext context)
    : DataRepository<TEntity, TeamDbContext, TEnum>(context), ITeamDataRepository<TEntity, TEnum>
     where TEntity : EnumDataEntity<TEnum>
     where TEnum : struct
{ }