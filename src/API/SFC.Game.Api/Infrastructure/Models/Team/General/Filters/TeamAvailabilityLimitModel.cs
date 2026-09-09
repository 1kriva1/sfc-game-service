using SFC.Game.Api.Infrastructure.Models.Common;
using SFC.Game.Application.Common.Dto.Team.General.Filters;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Team.General.Filters;

/// <summary>
/// Team **availability filter** model.
/// </summary>
public class TeamAvailabilityLimitModel :
    RangeLimitModel<TimeSpan?>,
    IMapTo<TeamAvailabilityLimitDto>
{
    /// <summary>
    /// Days of week.
    /// </summary>
    public IEnumerable<int>? Days { get; set; }
}