using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Queries.Find.Dto.Filters;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.General.Find.Filters;

/// <summary>
/// Get game teams for game team filter model.
/// </summary>
public class GetGameTeamsGameTeamFilterModel : IMapTo<GetGameTeamsGameTeamFilterDto>
{
    /// <summary>
    /// Statuses of game team.
    /// </summary>
    public IEnumerable<int> Statuses { get; set; } = default!;
}