using SFC.Game.Messages.Models.Common;

namespace SFC.Game.Messages.Models.Game.General;
public class Game : Auditable
{
    public long Id { get; set; }

    public Guid UserId { get; set; }

    public int StatusId { get; set; }

    public required GameGeneralProfile GeneralProfile { get; set; }

    public required GameFinancialProfile FinancialProfile { get; set; }

    public required GameInventaryProfile InventaryProfile { get; set; }

    public required GameAvailability Availability { get; init; }

    public IEnumerable<GameTag>? Tags { get; init; }
}