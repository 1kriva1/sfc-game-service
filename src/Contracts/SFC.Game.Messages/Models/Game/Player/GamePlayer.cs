using SFC.Game.Messages.Models.Common;

namespace SFC.Game.Messages.Models.Game.Player;
public class GamePlayer : Auditable
{
    public long Id { get; set; }

    public long GameId { get; set; }

    public long PlayerId { get; set; }

    public int StatusId { get; set; }

    public Guid UserId { get; set; }
}