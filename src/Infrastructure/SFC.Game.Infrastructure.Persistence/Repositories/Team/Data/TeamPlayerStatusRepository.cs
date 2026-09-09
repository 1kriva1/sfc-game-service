using SFC.Game.Application.Interfaces.Persistence.Repository.Team.Data;
using SFC.Game.Domain.Entities.Team.Data;
using SFC.Game.Infrastructure.Persistence.Contexts;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Team.Data;
public class TeamPlayerStatusRepository(TeamDbContext context)
    : TeamDataRepository<TeamPlayerStatus, TeamPlayerStatusEnum>(context), ITeamPlayerStatusRepository
{ }