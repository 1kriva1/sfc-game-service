using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Player.Common.Dto;
using SFC.Game.Domain.Entities.Game.Player;

namespace SFC.Game.Application.Features.Game.Player.Commands.Create;
public class CreateGamePlayerViewModel : IMapFrom<GamePlayer>
{
    public required GamePlayerDto GamePlayer { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<GamePlayer, CreateGamePlayerViewModel>()
                                                   .ForMember(p => p.GamePlayer, d => d.MapFrom(z => z));
}