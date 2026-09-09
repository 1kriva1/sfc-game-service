using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Team.General;

namespace SFC.Game.Application.Common.Dto.Team.General;
public class TeamAvailabilityDto : IMapFromReverse<TeamAvailability>
{
    public DayOfWeek Day { get; set; }

    public TimeSpan From { get; set; }

    public TimeSpan To { get; set; }
}