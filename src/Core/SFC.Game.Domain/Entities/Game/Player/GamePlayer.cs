using SFC.Game.Domain.Common;
using SFC.Game.Domain.Common.Interfaces;
using SFC.Game.Domain.Entities.Game.Team.General;

namespace SFC.Game.Domain.Entities.Game.Player;

public class GamePlayer : BaseAuditableEntity<long>, IPlayerEntity, IUserEntity
{
    public long GameId { get; set; }

    public long PlayerId { get; set; }

    public long? GameTeamId { get; set; }

    public Guid UserId { get; set; }

    public GamePlayerStatusEnum StatusId { get; set; }

    public PlayerEntity Player { get; set; } = default!;

    public GameTeam? GameTeam { get; set; }
}