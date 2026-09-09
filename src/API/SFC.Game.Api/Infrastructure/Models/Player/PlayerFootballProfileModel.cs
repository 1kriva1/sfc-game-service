using SFC.Game.Application.Common.Dto.Player.General;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Player;

/// <summary>
/// Player's **football** profile model.
/// </summary>
public class PlayerFootballProfileModel : IMapFrom<PlayerFootballProfileDto>
{
    /// <summary>
    /// Height.
    /// </summary>
    public int? Height { get; set; }

    /// <summary>
    /// Weight.
    /// </summary>
    public int? Weight { get; set; }

    /// <summary>
    /// Position on field.
    /// </summary>
    public int? Position { get; set; }

    /// <summary>
    /// Describe what **foot** player prefer to use.
    /// </summary>
    public int? WorkingFoot { get; set; }

    /// <summary>
    /// **Style** of play.
    /// </summary>
    public int? GameStyle { get; set; }

    /// <summary>
    /// **Dribbling** skill value.
    /// </summary>
    public int? Skill { get; set; }

    /// <summary>
    /// Physical condition value.
    /// </summary>
    public int? PhysicalCondition { get; set; }
}