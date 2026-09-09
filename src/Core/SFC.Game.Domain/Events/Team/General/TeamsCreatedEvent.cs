using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Events.Team.General;
public class TeamsCreatedEvent(IEnumerable<TeamEntity> teams) : BaseEvent
{
    public IEnumerable<TeamEntity> Teams { get; } = teams;
}