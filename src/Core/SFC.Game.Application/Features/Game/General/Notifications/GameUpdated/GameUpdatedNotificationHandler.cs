using MediatR;

using SFC.Game.Application.Interfaces.Game.General;
using SFC.Game.Domain.Events.Game.General;

namespace SFC.Game.Application.Features.Game.General.Notifications.GameUpdated;
public class GameUpdatedNotificationHandler(IGameService gameService) : INotificationHandler<GameUpdatedEvent>
{
    private readonly IGameService _gameService = gameService;

    public Task Handle(GameUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return _gameService.NotifyGameUpdatedAsync(notification.Game, cancellationToken);
    }
}