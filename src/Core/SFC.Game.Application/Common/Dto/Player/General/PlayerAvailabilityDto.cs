using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Player;

namespace SFC.Game.Application.Common.Dto.Player.General;

public record PlayerAvailabilityDto : IMapFromReverse<PlayerAvailability>
{
    public TimeSpan? From { get; set; }

    public TimeSpan? To { get; set; }

    public IEnumerable<DayOfWeek> Days { get; set; } = [];

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerAvailabilityDto, PlayerAvailability>()
               .ReverseMap()
               .IgnoreAllNonExisting();
    }
}