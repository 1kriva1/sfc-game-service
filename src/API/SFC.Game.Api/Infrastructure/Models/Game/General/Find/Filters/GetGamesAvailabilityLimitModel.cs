using SFC.Game.Api.Infrastructure.Models.Common;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Find.Filters;

/// <summary>
/// Get games **availability filter** model.
/// </summary>
public class GetGamesAvailabilityLimitModel :
    RangeLimitModel<TimeSpan?>,
    IMapTo<GetGamesAvailabilityLimitDto>
{
    /// <summary>
    /// Date of game play.
    /// </summary>
    public DateOnly? Date { get; set; }
}