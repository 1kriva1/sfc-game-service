using SFC.Game.Api.Infrastructure.Models.Common;
using SFC.Game.Application.Common.Dto.Player.General.Filters;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Player.Find.Filters;

/// <summary>
/// Get players **availability filter** model.
/// </summary>
public class PlayerAvailabilityLimitFilterModel :
    RangeLimitModel<TimeSpan?>,
    IMapTo<PlayerAvailabilityLimitFilterDto>
{
    /// <summary>
    /// Day of week.
    /// </summary>
    public IEnumerable<int> Days { get; set; } = default!;
}