using SFC.Game.Messages.Commands.Common;
using SFC.Game.Messages.Models.Game.Player;

namespace SFC.Game.Messages.Commands.Game.Player;
public class SeedGamePlayers : InitiatorCommand
{
    public IEnumerable<GamePlayer> GamePlayers { get; init; } = [];
}