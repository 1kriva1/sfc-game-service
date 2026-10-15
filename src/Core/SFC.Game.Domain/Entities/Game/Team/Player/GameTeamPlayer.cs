using SFC.Game.Domain.Common;
using SFC.Game.Domain.Common.Interfaces;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Team.Player;

namespace SFC.Game.Domain.Entities.Game.Team.Player;

public class GameTeamPlayer : BaseAuditableEntity<long>, IUserEntity
{
    public long GameTeamId { get; set; }

    public long? GamePlayerId { get; set; }

    public long? TeamPlayerId { get; set; }

    public Guid UserId { get; set; }

    public GameTeam GameTeam { get; set; } = default!;

    public GamePlayer? GamePlayer { get; set; }

    public TeamPlayer? TeamPlayer { get; set; }
}