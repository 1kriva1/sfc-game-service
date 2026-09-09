using SFC.Game.Domain.Entities.Team.Data;

namespace SFC.Game.Application.Interfaces.Persistence.Repository.Team.Data;
public interface ITeamPlayerStatusRepository : ITeamDataRepository<TeamPlayerStatus, TeamPlayerStatusEnum> { }