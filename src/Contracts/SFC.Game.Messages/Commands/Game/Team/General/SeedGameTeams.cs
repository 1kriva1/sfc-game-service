using SFC.Game.Messages.Commands.Common;
using SFC.Game.Messages.Models.Game.Team.General;

namespace SFC.Game.Messages.Commands.Game.Team.General;
public class SeedGameTeams : InitiatorCommand
{
    public IEnumerable<GameTeam> GameTeams { get; init; } = [];
}