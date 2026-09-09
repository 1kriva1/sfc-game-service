using SFC.Game.Domain.Common;
using SFC.Game.Domain.Entities.Game.Team.Player;

namespace SFC.Game.Domain.Events.Game.Team.Player;
public class GameTeamPlayerUpdatedEvent(GameTeamPlayer entity) : BaseEvent
{
    public GameTeamPlayer GameTeamPlayer { get; } = entity;
}