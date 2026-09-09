using MediatR;

using SFC.Game.Application.Interfaces.Game.Team.General;
using SFC.Game.Domain.Events.Game.Team.General;

namespace SFC.Game.Application.Features.Game.Team.General.Notifications.GameTeamUpdated;
public class GameTeamUpdatedNotificationHandler(IGameTeamService gameTeamService) : INotificationHandler<GameTeamUpdatedEvent>
{
    private readonly IGameTeamService _gameTeamService = gameTeamService;

    public Task Handle(GameTeamUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return _gameTeamService.NotifyGameTeamUpdatedAsync(notification.GameTeam, cancellationToken);
    }
}