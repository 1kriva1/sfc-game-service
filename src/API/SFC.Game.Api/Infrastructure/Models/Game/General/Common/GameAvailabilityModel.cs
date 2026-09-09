using SFC.Game.Api.Infrastructure.Models.Common;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Common.Dto;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Common;

/// <summary>
/// Game's **availability** model (when game is available to play).
/// </summary>
public class GameAvailabilityModel :
    RangeLimitModel<TimeSpan?>,
    IMapFromReverse<GameAvailabilityDto>
{
    /// <summary>
    /// Date.
    /// </summary>
    public DateOnly Date { get; set; }
}