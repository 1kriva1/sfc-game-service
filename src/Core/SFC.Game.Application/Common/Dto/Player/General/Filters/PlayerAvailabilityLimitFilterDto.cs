using SFC.Game.Application.Features.Common.Dto.Common;

namespace SFC.Game.Application.Common.Dto.Player.General.Filters;
public class PlayerAvailabilityLimitFilterDto : RangeLimitDto<TimeSpan?>
{
    public IEnumerable<DayOfWeek> Days { get; set; } = [];
}