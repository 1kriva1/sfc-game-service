using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Events.Player;
public class PlayersCreatedEvent(IEnumerable<PlayerEntity> players) : BaseEvent
{
    public IEnumerable<PlayerEntity> Players { get; } = players;
}