using MediatR;

using SFC.Game.Application.Interfaces.Game.General;
using SFC.Game.Domain.Events.Game.General;

namespace SFC.Game.Application.Features.Game.General.Notifications.GameCreated;
public class GameCreatedNotificationHandler(IGameService gameService) : INotificationHandler<GameCreatedEvent>
{
    private readonly IGameService _gameService = gameService;

    public Task Handle(GameCreatedEvent notification, CancellationToken cancellationToken)
    {
        return _gameService.NotifyGameCreatedAsync(notification.Game, cancellationToken);
    }
}