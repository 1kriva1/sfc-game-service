using SFC.Game.Messages.Models.Game.Player;

namespace SFC.Game.Messages.Events.Game.Player;
public class GamePlayersSeeded
{
    public IEnumerable<GamePlayer> GamePlayers { get; init; } = [];
}