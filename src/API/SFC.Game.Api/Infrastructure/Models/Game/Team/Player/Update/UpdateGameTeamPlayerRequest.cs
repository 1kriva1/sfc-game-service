using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Update;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Update;

/// <summary>
/// **Update** game team player request.
/// </summary>
public class UpdateGameTeamPlayerRequest : IMapTo<UpdateGameTeamPlayerCommand>
{
    /// <summary>
    /// Game team player model.
    /// </summary>
    public UpdateGameTeamPlayerModel GameTeamPlayer { get; set; } = default!;

    public void Mapping(Profile profile) => profile.CreateMap<UpdateGameTeamPlayerRequest, UpdateGameTeamPlayerCommand>()
                                                   .IgnoreAllNonExisting();
}