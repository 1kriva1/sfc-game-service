using SFC.Game.Application.Common.Dto.Player.General.Filters;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Player.Find.Filters;

/// <summary>
/// Get players filter model.
/// </summary>
public class PlayerFilterModel : IMapTo<PlayerFilterDto>
{
    /// <summary>
    /// Profile filter model.
    /// </summary>
    public PlayerProfileFilterModel Profile { get; set; } = default!;

    /// <summary>
    /// Stats filter model.
    /// </summary>
    public PlayerStatsFilterModel Stats { get; set; } = default!;
}