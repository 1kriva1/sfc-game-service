using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Player;
public class PlayerAvailableDay : BaseEntity<long>
{
    public PlayerAvailability Availability { get; set; } = null!;

    public DayOfWeek Day { get; set; }
}