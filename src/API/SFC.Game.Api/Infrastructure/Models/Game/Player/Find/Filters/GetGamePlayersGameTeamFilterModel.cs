using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Player.Queries.Find.Dto.Filters;

namespace SFC.Game.Api.Infrastructure.Models.Game.Player.Find.Filters;

/// <summary>
/// Get game players for game player filter model.
/// </summary>
public class GetGamePlayersGamePlayerFilterModel : IMapTo<GetGamePlayersGamePlayerFilterDto>
{
    /// <summary>
    /// Statuses of game player.
    /// </summary>
    public IEnumerable<int> Statuses { get; set; } = default!;
}