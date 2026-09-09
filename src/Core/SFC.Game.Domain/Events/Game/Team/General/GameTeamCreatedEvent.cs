using SFC.Game.Domain.Common;
using SFC.Game.Domain.Entities.Game.Team.General;

namespace SFC.Game.Domain.Events.Game.Team.General;
public class GameTeamCreatedEvent(GameTeam entity) : BaseEvent
{
    public GameTeam GameTeam { get; } = entity;
}