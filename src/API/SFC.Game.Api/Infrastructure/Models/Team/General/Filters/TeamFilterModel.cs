using SFC.Game.Application.Common.Dto.Team.General.Filters;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Team.General.Filters;

/// <summary>
/// Team filter model.
/// </summary>
public class TeamFilterModel : IMapTo<TeamFilterDto>
{
    /// <summary>
    /// Statuses of team.
    /// </summary>
    public IEnumerable<int> Statuses { get; set; } = default!;

    /// <summary>
    /// Profile filter model.
    /// </summary>
    public TeamProfileFilterModel? Profile { get; set; }
}