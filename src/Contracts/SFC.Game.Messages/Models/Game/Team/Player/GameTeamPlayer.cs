using SFC.Game.Messages.Models.Common;

namespace SFC.Game.Messages.Models.Game.Team.Player;
public class GameTeamPlayer : Auditable
{
    public long Id { get; set; }

    public long GameTeamId { get; set; }

    public long? GamePlayerId { get; set; }

    public long? TeamPlayerId { get; set; }

    public Guid UserId { get; set; }
}