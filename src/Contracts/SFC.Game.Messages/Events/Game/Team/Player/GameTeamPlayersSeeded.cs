using SFC.Game.Messages.Models.Game.Team.Player;

namespace SFC.Game.Messages.Events.Game.Team.Player;
public class GameTeamPlayersSeeded
{
    public IEnumerable<GameTeamPlayer> GameTeamPlayers { get; init; } = [];
}