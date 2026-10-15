using AutoMapper;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Deletes;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Deletes;

/// <summary>
/// **Deletes** game team player request.
/// </summary>
public class DeletesGameTeamPlayerRequest : IMapTo<DeletesGameTeamPlayerCommand>
{
    /// <summary>
    /// Game team player models.
    /// </summary>
    public IEnumerable<DeletesGameTeamPlayerModel> GameTeamPlayers { get; set; } = default!;

    public void Mapping(Profile profile) => profile.CreateMap<DeletesGameTeamPlayerRequest, DeletesGameTeamPlayerCommand>()
                                                   .IgnoreAllNonExisting();
}