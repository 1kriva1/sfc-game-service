using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Commands.Create;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.General.Create;

/// <summary>
/// **Create** game team model.
/// </summary>
public class CreateGameTeamModel : IMapTo<CreateGameTeamDto>
{
    public long Team { get; set; }

    public int? Status { get; set; }

    public int? Index { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<CreateGameTeamModel, CreateGameTeamDto>()
                                                   .ForMember(p => p.TeamId, d => d.MapFrom(z => z.Team))
                                                   .ForMember(p => p.StatusId, d => d.MapFrom(z => z.Status));
}