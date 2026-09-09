using SFC.Game.Domain.Common;
using SFC.Game.Domain.Entities.Team.Player;

namespace SFC.Game.Domain.Events.Team.Player;
public class TeamPlayersCreatedEvent(IEnumerable<TeamPlayer> teamPlayers) : BaseEvent
{
    public IEnumerable<TeamPlayer> TeamPlayers { get; } = teamPlayers;
}