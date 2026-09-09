using SFC.Game.Application.Features.Common.Dto.Common;

namespace SFC.Game.Application.Common.Dto.Team.General.Filters;
public class TeamAvailabilityLimitDto : RangeLimitDto<TimeSpan?>
{
    public IEnumerable<DayOfWeek> Days { get; set; } = [];
}