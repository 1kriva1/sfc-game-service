using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Commands.Create;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.General.Create;

/// <summary>
/// **Create** Game Team request.
/// </summary>
public class CreateGameTeamRequest : IMapTo<CreateGameTeamCommand>
{
    /// <summary>
    /// Game Team model.
    /// </summary>
    public CreateGameTeamModel GameTeam { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreateGameTeamRequest, CreateGameTeamCommand>()
                                                   .IgnoreAllNonExisting();
}