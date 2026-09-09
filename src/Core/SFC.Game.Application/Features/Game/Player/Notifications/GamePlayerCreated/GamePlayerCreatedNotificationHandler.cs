using MediatR;

using SFC.Game.Application.Interfaces.Game.Player;
using SFC.Game.Domain.Events.Game.Player;

namespace SFC.Game.Application.Features.Game.Player.Notifications.GamePlayerCreated;
public class GamePlayerCreatedNotificationHandler(IGamePlayerService gamePlayerService) : INotificationHandler<GamePlayerCreatedEvent>
{
    private readonly IGamePlayerService _gamePlayerService = gamePlayerService;

    public Task Handle(GamePlayerCreatedEvent notification, CancellationToken cancellationToken)
    {
        return _gamePlayerService.NotifyGamePlayerCreatedAsync(notification.GamePlayer, cancellationToken);
    }
}