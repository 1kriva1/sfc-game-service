using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Updates;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Updates;

/// <summary>
/// **Updates** game team player request.
/// </summary>
public class UpdatesGameTeamPlayerRequest : IMapTo<UpdatesGameTeamPlayerCommand>
{
    /// <summary>
    /// Game team player models.
    /// </summary>
    public IEnumerable<UpdatesGameTeamPlayerModel> GameTeamPlayers { get; set; } = default!;

    public void Mapping(Profile profile) => profile.CreateMap<UpdatesGameTeamPlayerRequest, UpdatesGameTeamPlayerCommand>()
                                                   .IgnoreAllNonExisting();
}