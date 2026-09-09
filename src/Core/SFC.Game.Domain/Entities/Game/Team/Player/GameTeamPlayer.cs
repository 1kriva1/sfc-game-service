using SFC.Game.Domain.Common;
using SFC.Game.Domain.Common.Interfaces;
using SFC.Game.Domain.Entities.Game.Team.General;

namespace SFC.Game.Domain.Entities.Game.Team.Player;

public class GameTeamPlayer : BaseAuditableEntity<long>, IPlayerEntity, IUserEntity
{
    public long GameTeamId { get; set; }

    public long PlayerId { get; set; }

    public Guid UserId { get; set; }

    public TeamPlayerStatusEnum StatusId { get; set; }

    public GameTeam GameTeam { get; set; } = default!;

    public PlayerEntity Player { get; set; } = default!;
}