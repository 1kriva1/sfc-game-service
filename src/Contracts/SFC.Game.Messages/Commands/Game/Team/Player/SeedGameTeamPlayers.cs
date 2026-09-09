using SFC.Game.Messages.Commands.Common;
using SFC.Game.Messages.Models.Game.Team.Player;

namespace SFC.Game.Messages.Commands.Game.Team.Player;
public class SeedGameTeamPlayers : InitiatorCommand
{
    public IEnumerable<GameTeamPlayer> GameTeamPlayers { get; init; } = [];
}