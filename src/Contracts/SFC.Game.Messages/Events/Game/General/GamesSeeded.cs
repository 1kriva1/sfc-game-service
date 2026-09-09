namespace SFC.Game.Messages.Events.Game.General;
public class GamesSeeded
{
    public IEnumerable<GameEntity> Games { get; init; } = [];
}