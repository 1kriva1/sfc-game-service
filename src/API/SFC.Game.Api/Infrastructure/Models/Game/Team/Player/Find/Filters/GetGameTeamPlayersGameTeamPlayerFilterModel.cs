using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Find.Dto.Filters;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Find.Filters;

/// <summary>
/// Get game team players for game team filter model.
/// </summary>
public class GetGameTeamPlayersGameTeamPlayerFilterModel : IMapTo<GetGameTeamPlayersGameTeamPlayerFilterDto>
{
    /// <summary>
    /// Statuses of game team player.
    /// </summary>
    public IEnumerable<int> Statuses { get; set; } = default!;
}