using SFC.Game.Messages.Models.Common;

namespace SFC.Game.Messages.Models.Game.Team.General;
public class GameTeam : Auditable
{
    public long Id { get; set; }

    public long GameId { get; set; }

    public long TeamId { get; set; }

    public int StatusId { get; set; }

    public int Index { get; set; }

    public Guid UserId { get; set; }
}