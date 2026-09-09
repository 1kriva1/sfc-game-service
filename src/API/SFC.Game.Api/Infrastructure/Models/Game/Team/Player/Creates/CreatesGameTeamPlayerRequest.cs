using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Creates;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Creates;

/// <summary>
/// **Creates** Game Team Player request.
/// </summary>
public class CreatesGameTeamPlayerRequest : IMapTo<CreatesGameTeamPlayerCommand>
{
    /// <summary>
    /// Game Team Player models.
    /// </summary>
    public IEnumerable<CreatesGameTeamPlayerModel> GameTeamPlayers { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreatesGameTeamPlayerRequest, CreatesGameTeamPlayerCommand>()
                                                   .IgnoreAllNonExisting();
}