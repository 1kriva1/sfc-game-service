using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Common.Dto;
using SFC.Game.Domain.Entities.Game.Team.General;

namespace SFC.Game.Application.Features.Game.Team.General.Queries.Gets;
public class GetsGameTeamViewModel : IMapFrom<IEnumerable<GameTeam>>
{
    public required IEnumerable<GameTeamDto> GameTeams { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<IEnumerable<GameTeam>, GetsGameTeamViewModel>()
                                                   .ForMember(p => p.GameTeams, d => d.MapFrom(z => z));
}