using SFC.Game.Application.Common.Dto.Team.General.Filters;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Team.General.Filters;

/// <summary>
/// Team **profile filter** model.
/// </summary>
public class TeamProfileFilterModel : IMapTo<TeamProfileFilterDto>
{
    /// <summary>
    /// General profile.
    /// </summary>
    public TeamGeneralProfileFilterModel? General { get; set; }

    /// <summary>
    /// Financial profile.
    /// </summary>
    public TeamFinancialProfileFilterModel? Financial { get; set; }

    /// <summary>
    /// Inventary profile.
    /// </summary>
    public TeamInventaryProfileFilterModel? Inventary { get; set; }
}