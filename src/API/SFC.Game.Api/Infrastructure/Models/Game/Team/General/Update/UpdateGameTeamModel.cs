using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Commands.Update;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.General.Update;

/// <summary>
/// **Update** game team model.
/// </summary>
public class UpdateGameTeamModel : IMapTo<UpdateGameTeamDto>
{
    /// <summary>
    /// Status of game team.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Index of game team.
    /// </summary>
    public int? Index { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<UpdateGameTeamModel, UpdateGameTeamDto>()
                                                   .ForMember(p => p.StatusId, d => d.MapFrom(z => z.Status));
}