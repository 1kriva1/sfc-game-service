using MediatR;

using SFC.Game.Application.Interfaces.Game.Player;
using SFC.Game.Domain.Events.Game.Player;

namespace SFC.Game.Application.Features.Game.Player.Notifications.GamePlayerUpdated;
public class GamePlayerUpdatedNotificationHandler(IGamePlayerService gamePlayerService) : INotificationHandler<GamePlayerUpdatedEvent>
{
    private readonly IGamePlayerService _gamePlayerService = gamePlayerService;

    public Task Handle(GamePlayerUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return _gamePlayerService.NotifyGamePlayerUpdatedAsync(notification.GamePlayer, cancellationToken);
    }
}