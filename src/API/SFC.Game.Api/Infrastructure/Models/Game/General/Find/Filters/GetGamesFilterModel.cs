using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Find.Filters;

/// <summary>
/// Get games filter model.
/// </summary>
public class GetGamesFilterModel : IMapTo<GetGamesFilterDto>
{
    /// <summary>
    /// Statuses of game.
    /// </summary>
    public IEnumerable<int> Statuses { get; set; } = default!;

    /// <summary>
    /// Profile filter model.
    /// </summary>
    public GetGamesProfileFilterModel? Profile { get; set; }
}