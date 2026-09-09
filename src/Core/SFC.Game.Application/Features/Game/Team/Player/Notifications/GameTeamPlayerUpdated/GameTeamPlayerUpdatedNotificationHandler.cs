using MediatR;

using SFC.Game.Application.Interfaces.Game.Team.Player;
using SFC.Game.Domain.Events.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Notifications.GameTeamPlayerUpdated;
public class GameTeamPlayerUpdatedNotificationHandler(IGameTeamPlayerService gameTeamPlayerService) : INotificationHandler<GameTeamPlayerUpdatedEvent>
{
    private readonly IGameTeamPlayerService _gameTeamPlayerService = gameTeamPlayerService;

    public Task Handle(GameTeamPlayerUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return _gameTeamPlayerService.NotifyGameTeamPlayerUpdatedAsync(notification.GameTeamPlayer, cancellationToken);
    }
}