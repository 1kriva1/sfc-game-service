using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Player;

namespace SFC.Game.Application.Common.Dto.Player.General;

public record PlayerStatPointsDto : IMapFromReverse<PlayerStatPoints>
{
    public int Available { get; set; }

    public int Used { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerStatPointsDto, PlayerStatPoints>()
               .ReverseMap()
               .IgnoreAllNonExisting();
    }
}