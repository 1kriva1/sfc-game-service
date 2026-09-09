using SFC.Game.Domain.Entities.Game.Team.Player;

namespace SFC.Game.Application.Interfaces.Game.Team.Player;
public interface IGameTeamPlayerService
{
    Task NotifyGameTeamPlayerCreatedAsync(GameTeamPlayer gameTeamPlayer, CancellationToken cancellationToken = default);

    Task NotifyGameTeamPlayerUpdatedAsync(GameTeamPlayer gameTeamPlayer, CancellationToken cancellationToken = default);
}