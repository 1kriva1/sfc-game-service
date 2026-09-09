using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Common.Dto;

namespace SFC.Game.Application.Features.Game.General.Queries.Get;
public class GetGameViewModel : IMapFrom<GameEntity>
{
    public required GameDto Game { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<GameEntity, GetGameViewModel>()
                                                   .ForMember(p => p.Game, d => d.MapFrom(z => z));
}