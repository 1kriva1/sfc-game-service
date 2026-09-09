using SFC.Game.Application.Features.Common.Dto.Common;

namespace SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;
public class GetGamesAvailabilityLimitDto : RangeLimitDto<TimeSpan?>
{
    public DateOnly? Date { get; set; }
}