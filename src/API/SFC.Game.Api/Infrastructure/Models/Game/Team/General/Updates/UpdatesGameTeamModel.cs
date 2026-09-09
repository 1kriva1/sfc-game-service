using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Commands.Updates;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.General.Updates;

/// <summary>
/// **Updates** game team models.
/// </summary>
public class UpdatesGameTeamModel : IMapTo<UpdatesGameTeamDto>
{
    /// <summary>
    /// Status of game team.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Index of game team.
    /// </summary>
    public int? Index { get; set; }

    /// <summary>
    /// Team unique identifier.
    /// </summary>
    public long Team { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<UpdatesGameTeamModel, UpdatesGameTeamDto>()
                                                   .ForMember(p => p.StatusId, d => d.MapFrom(z => z.Status))
                                                   .ForMember(p => p.TeamId, d => d.MapFrom(z => z.Team));
}