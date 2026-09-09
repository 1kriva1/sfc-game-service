using SFC.Game.Domain.Common;
using SFC.Game.Domain.Entities.Game.Player;

namespace SFC.Game.Domain.Events.Game.Player;
public class GamePlayerCreatedEvent(GamePlayer entity) : BaseEvent
{
    public GamePlayer GamePlayer { get; } = entity;
}