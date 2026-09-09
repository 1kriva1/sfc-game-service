using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Commands.Update;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.General.Update;

/// <summary>
/// **Update** game team request.
/// </summary>
public class UpdateGameTeamRequest : IMapTo<UpdateGameTeamCommand>
{
    /// <summary>
    /// Game team model.
    /// </summary>
    public UpdateGameTeamModel GameTeam { get; set; } = default!;

    public void Mapping(Profile profile) => profile.CreateMap<UpdateGameTeamRequest, UpdateGameTeamCommand>()
                                                   .IgnoreAllNonExisting();
}