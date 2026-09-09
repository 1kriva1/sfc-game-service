using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Common.Dto;
using SFC.Game.Domain.Entities.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Creates;
public class CreatesGameTeamPlayerViewModel : IMapFrom<IEnumerable<GameTeamPlayer>>
{
    public required IEnumerable<GameTeamPlayerDto> GameTeamPlayers { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<IEnumerable<GameTeamPlayer>, CreatesGameTeamPlayerViewModel>()
                                                   .ForMember(p => p.GameTeamPlayers, d => d.MapFrom(z => z));
}