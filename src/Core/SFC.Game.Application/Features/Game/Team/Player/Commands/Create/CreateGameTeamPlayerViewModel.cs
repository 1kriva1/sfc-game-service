using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Common.Dto;
using SFC.Game.Domain.Entities.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Create;
public class CreateGameTeamPlayerViewModel : IMapFrom<GameTeamPlayer>
{
    public required GameTeamPlayerDto GameTeamPlayer { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<GameTeamPlayer, CreateGameTeamPlayerViewModel>()
                                                   .ForMember(p => p.GameTeamPlayer, d => d.MapFrom(z => z));
}