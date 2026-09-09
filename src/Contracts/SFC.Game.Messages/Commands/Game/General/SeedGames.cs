using SFC.Game.Messages.Commands.Common;

namespace SFC.Game.Messages.Commands.Game.General;
public class SeedGames : InitiatorCommand
{
    public IEnumerable<GameEntity> Games { get; init; } = [];
}