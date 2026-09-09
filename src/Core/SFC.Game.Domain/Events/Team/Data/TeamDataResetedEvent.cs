using SFC.Game.Domain.Common;
using SFC.Game.Domain.Entities.Team.Data;

namespace SFC.Game.Domain.Events.Team.Data;

public class TeamDataResetedEvent(IEnumerable<TeamPlayerStatus> teamPlayerStatuses) : BaseEvent
{
    public IEnumerable<TeamPlayerStatus> TeamPlayerStatuses { get; } = teamPlayerStatuses;
}