using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Game.General;

namespace SFC.Game.Application.Features.Game.General.Common.Dto;
public class GameAvailabilityDto : IMapFromReverse<GameAvailability>
{
    public DateOnly Date { get; set; }

    public TimeSpan From { get; set; }

    public TimeSpan To { get; set; }
}