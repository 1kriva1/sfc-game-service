using MediatR;

using SFC.Game.Application.Interfaces.Game.Team.Player;
using SFC.Game.Domain.Events.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Notifications.GameTeamPlayerDeleted;
public class GameTeamPlayerDeletedNotificationHandler(IGameTeamPlayerService gameTeamPlayerService) : INotificationHandler<GameTeamPlayerDeletedEvent>
{
    private readonly IGameTeamPlayerService _gameTeamPlayerService = gameTeamPlayerService;

    public Task Handle(GameTeamPlayerDeletedEvent notification, CancellationToken cancellationToken)
    {
        return _gameTeamPlayerService.NotifyGameTeamPlayerDeletedAsync(notification.GameTeamPlayer, cancellationToken);
    }
}