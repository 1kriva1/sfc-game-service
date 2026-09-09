using SFC.Game.Application.Common.Dto.Team.General;

namespace SFC.Game.Application.Interfaces.Team.General;
public interface ITeamService
{
    Task<TeamDto?> GetTeamAsync(long id, CancellationToken cancellationToken = default);
}