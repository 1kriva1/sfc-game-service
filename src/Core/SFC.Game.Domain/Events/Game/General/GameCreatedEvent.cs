using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Events.Game.General;
public class GameCreatedEvent(GameEntity entity) : BaseEvent
{
    public GameEntity Game { get; } = entity;
}