using MediatR;

using SFC.Game.Application.Interfaces.Game.Team.General;
using SFC.Game.Domain.Events.Game.Team.General;

namespace SFC.Game.Application.Features.Game.Team.General.Notifications.GameTeamCreated;
public class GameTeamCreatedNotificationHandler(IGameTeamService gameTeamService) : INotificationHandler<GameTeamCreatedEvent>
{
    private readonly IGameTeamService _gameTeamService = gameTeamService;

    public Task Handle(GameTeamCreatedEvent notification, CancellationToken cancellationToken)
    {
        return _gameTeamService.NotifyGameTeamCreatedAsync(notification.GameTeam, cancellationToken);
    }
}