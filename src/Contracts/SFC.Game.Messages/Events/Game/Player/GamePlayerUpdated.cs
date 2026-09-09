using SFC.Game.Messages.Models.Game.Player;

namespace SFC.Game.Messages.Events.Game.Player;
public class GamePlayerUpdated
{
    public required GamePlayer GamePlayer { get; set; }
}