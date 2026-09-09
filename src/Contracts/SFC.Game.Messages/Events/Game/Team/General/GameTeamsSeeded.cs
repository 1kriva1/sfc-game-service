using SFC.Game.Messages.Models.Game.Team.General;

namespace SFC.Game.Messages.Events.Game.Team.General;
public class GameTeamsSeeded
{
    public IEnumerable<GameTeam> GameTeams { get; init; } = [];
}