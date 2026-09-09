using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Common.Dto;
using SFC.Game.Domain.Entities.Game.Team.General;

namespace SFC.Game.Application.Features.Game.Team.General.Commands.Create;
public class CreateGameTeamViewModel : IMapFrom<GameTeam>
{
    public required GameTeamDto GameTeam { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<GameTeam, CreateGameTeamViewModel>()
                                                   .ForMember(p => p.GameTeam, d => d.MapFrom(z => z));
}