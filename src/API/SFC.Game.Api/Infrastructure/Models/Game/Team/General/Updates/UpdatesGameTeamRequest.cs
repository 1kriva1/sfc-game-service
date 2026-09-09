using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Commands.Updates;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.General.Updates;

/// <summary>
/// **Updates** game team request.
/// </summary>
public class UpdatesGameTeamRequest : IMapTo<UpdatesGameTeamCommand>
{
    /// <summary>
    /// Game team models.
    /// </summary>
    public IEnumerable<UpdatesGameTeamModel> GameTeams { get; set; } = default!;

    public void Mapping(Profile profile) => profile.CreateMap<UpdatesGameTeamRequest, UpdatesGameTeamCommand>()
                                                   .IgnoreAllNonExisting();
}