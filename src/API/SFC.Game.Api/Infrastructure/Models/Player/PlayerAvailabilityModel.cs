using SFC.Game.Api.Infrastructure.Models.Common;
using SFC.Game.Application.Common.Dto.Player.General;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Player;

/// <summary>
/// Player's **availability** model (when player is available to play).
/// </summary>
public class PlayerAvailabilityModel :
    RangeLimitModel<TimeSpan?>,
    IMapFromReverse<PlayerAvailabilityDto>
{
    /// <summary>
    /// Days of week.
    /// </summary>
    public IEnumerable<DayOfWeek> Days { get; set; } = [];
}