using SFC.Game.Application.Common.Dto.Team.General;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Team.General;

/// <summary>
/// Team **profile** model.
/// </summary>
public class TeamProfileModel : IMapFromReverse<TeamProfileDto>
{
    /// <summary>
    /// General profile.
    /// </summary>
    public required TeamGeneralProfileModel General { get; set; }

    /// <summary>
    /// Financial profile.
    /// </summary>
    public required TeamFinancialProfileModel Financial { get; set; }

    /// <summary>
    /// Inventary profile.
    /// </summary>
    public required TeamInventaryProfileModel Inventary { get; set; }
}