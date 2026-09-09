using SFC.Game.Domain.Common;
using SFC.Game.Domain.Common.Interfaces;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Team.Player;

namespace SFC.Game.Domain.Entities.Game.General;

/// <summary>
/// Core entity of the service.
/// </summary>
public class Game : BaseAuditableEntity<long>, IUserEntity
{
    public Guid UserId { get; set; }

    public GameStatusEnum StatusId { get; set; }

    public required GameGeneralProfile GeneralProfile { get; set; }

    public required GameFinancialProfile FinancialProfile { get; set; }

    public required GameInventaryProfile InventaryProfile { get; set; }

    public required GameAvailability Availability { get; set; }

    public ICollection<GameTag> Tags { get; } = [];

    public ICollection<GameTeam> Teams { get; } = [];

    public ICollection<GamePlayer> Players { get; } = [];
}