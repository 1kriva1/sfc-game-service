using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Team.General.Common;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Commands.Create;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.General.Create;

/// <summary>
/// **Create** game team response model.
/// </summary>
public class CreateGameTeamResponse :
    BaseErrorResponse, IMapFrom<CreateGameTeamViewModel>
{
    /// <summary>
    /// Game team model.
    /// </summary>
    public GameTeamModel GameTeam { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreateGameTeamViewModel, CreateGameTeamResponse>()
                                                   .IgnoreAllNonExisting();
}