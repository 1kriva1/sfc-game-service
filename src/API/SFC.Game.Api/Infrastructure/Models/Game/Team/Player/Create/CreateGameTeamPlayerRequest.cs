using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Create;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Create;

/// <summary>
/// **Create** Game Team Player request.
/// </summary>
public class CreateGameTeamPlayerRequest : IMapTo<CreateGameTeamPlayerCommand>
{
    /// <summary>
    /// Game Team Player model.
    /// </summary>
    public CreateGameTeamPlayerModel GameTeamPlayer { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreateGameTeamPlayerRequest, CreateGameTeamPlayerCommand>()
                                                   .IgnoreAllNonExisting();
}