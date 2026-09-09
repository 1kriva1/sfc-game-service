using SFC.Game.Api.Infrastructure.Models.Common;
using SFC.Game.Application.Common.Dto.Player.General.Filters;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Player.Find.Filters;

/// <summary>
/// Get players **football profile filter** model.
/// </summary>
public class PlayerFootballProfileFilterModel : IMapTo<PlayerFootballProfileFilterDto>
{
    /// <summary>
    /// Height.
    /// </summary>
    public RangeLimitModel<short?> Height { get; set; } = default!;

    /// <summary>
    /// Weight.
    /// </summary>
    public RangeLimitModel<short?> Weight { get; set; } = default!;

    /// <summary>
    /// List of **positions** on field.
    /// </summary>
    public IEnumerable<int> Positions { get; set; } = default!;

    /// <summary>
    /// What **foot** player prefer to use.
    /// </summary>
    public int? WorkingFoot { get; set; }

    /// <summary>
    /// **Style** of play.
    /// </summary>
    public IEnumerable<int> GameStyles { get; set; } = default!;

    /// <summary>
    /// **Dribbling** skill value.
    /// </summary>
    public int? Skill { get; set; }

    /// <summary>
    /// Physical condition value.
    /// </summary>
    public int? PhysicalCondition { get; set; }
}