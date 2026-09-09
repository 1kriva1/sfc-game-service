using MediatR;

using SFC.Game.Application.Interfaces.Game.Team.Player;
using SFC.Game.Domain.Events.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Notifications.GameTeamPlayerCreated;
public class GameTeamPlayerCreatedNotificationHandler(IGameTeamPlayerService gameTeamPlayerService) : INotificationHandler<GameTeamPlayerCreatedEvent>
{
    private readonly IGameTeamPlayerService _gameTeamPlayerService = gameTeamPlayerService;

    public Task Handle(GameTeamPlayerCreatedEvent notification, CancellationToken cancellationToken)
    {
        return _gameTeamPlayerService.NotifyGameTeamPlayerCreatedAsync(notification.GameTeamPlayer, cancellationToken);
    }
}