using SFC.Game.Domain.Entities.Game.Team.General;

namespace SFC.Game.Application.Interfaces.Game.Team.General;
public interface IGameTeamService
{
    Task NotifyGameTeamCreatedAsync(GameTeam gameTeam, CancellationToken cancellationToken = default);

    Task NotifyGameTeamUpdatedAsync(GameTeam gameTeam, CancellationToken cancellationToken = default);
}