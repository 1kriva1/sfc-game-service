using SFC.Game.Domain.Common;
using SFC.Game.Domain.Common.Interfaces;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Enums.Game;

namespace SFC.Game.Domain.Entities.Game.Team.General;

public class GameTeam : BaseAuditableEntity<long>, ITeamEntity, IUserEntity
{
    public GameTeamIndex? Index { get; set; }

    public GameTeamStatusEnum StatusId { get; set; }

    public long GameId { get; set; }

    public long TeamId { get; set; }

    public Guid UserId { get; set; }

    public TeamEntity Team { get; set; } = default!;

    public ICollection<GameTeamPlayer> Players { get; } = [];
}