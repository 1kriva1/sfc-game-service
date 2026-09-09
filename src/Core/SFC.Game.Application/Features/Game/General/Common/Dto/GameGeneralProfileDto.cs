using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Game.General;

namespace SFC.Game.Application.Features.Game.General.Common.Dto;
public class GameGeneralProfileDto : IMapFrom<GameEntity>, IMapTo<GameGeneralProfile>
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public long? LocationId { get; set; }

    public required GameAvailabilityDto Availability { get; set; }

    public IEnumerable<string> Tags { get; set; } = [];

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GameEntity, GameGeneralProfileDto>()
               .ForMember(p => p.Name, d => d.MapFrom(z => z.GeneralProfile.Name))
               .ForMember(p => p.Description, d => d.MapFrom(z => z.GeneralProfile.Description))
               .ForMember(p => p.LocationId, d => d.MapFrom(z => z.GeneralProfile.LocationId))
               .ReverseMap();

        profile.CreateMap<GameGeneralProfileDto, GameGeneralProfile>()
               .IgnoreAllNonExisting();
    }
}