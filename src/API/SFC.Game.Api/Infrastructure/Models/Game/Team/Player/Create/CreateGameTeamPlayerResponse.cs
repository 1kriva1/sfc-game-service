using AutoMapper;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Common;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Create;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Create;

/// <summary>
/// **Create** game team player response model.
/// </summary>
public class CreateGameTeamPlayerResponse :
    BaseErrorResponse, IMapFrom<CreateGameTeamPlayerViewModel>
{
    /// <summary>
    /// Game team player model.
    /// </summary>
    public GameTeamPlayerModel GameTeamPlayer { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreateGameTeamPlayerViewModel, CreateGameTeamPlayerResponse>()
                                                   .IgnoreAllNonExisting();
}