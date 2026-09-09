using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Team.General;

namespace SFC.Game.Application.Common.Dto.Team.General;

public class TeamInventaryProfileDto : IMapToReverse<TeamInventaryProfile>
{
    public IEnumerable<int> Shirts { get; set; } = [];

    public bool HasManiches { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TeamEntity, TeamInventaryProfileDto>();

        profile.CreateMap<TeamInventaryProfileDto, TeamInventaryProfile>()
               .ReverseMap()
               .IgnoreAllNonExisting();
    }
}