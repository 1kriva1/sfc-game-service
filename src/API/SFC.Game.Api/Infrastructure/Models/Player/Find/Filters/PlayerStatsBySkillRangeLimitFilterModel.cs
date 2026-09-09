using SFC.Game.Api.Infrastructure.Models.Common;
using SFC.Game.Application.Common.Dto.Player.General.Filters;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Player.Find.Filters;

/// <summary>
/// Range limit by **stat skill**.
/// </summary>
public class PlayerStatsBySkillRangeLimitFilterModel :
    RangeLimitModel<short?>,
    IMapTo<PlayerStatsBySkillRangeLimitFilterDto>
{
    /// <summary>
    /// Stat skill unique identifier.
    /// </summary>
    public int Skill { get; set; }
}