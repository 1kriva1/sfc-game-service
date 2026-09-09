using SFC.Game.Application.Common.Dto.Player.General;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Player;

/// <summary>
/// Player model.
/// </summary>
public class PlayerModel : IMapFrom<PlayerDto>
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Player's profile model.
    /// </summary>
    public PlayerProfileModel Profile { get; set; } = null!;

    /// <summary>
    /// Player's stats model.
    /// </summary>
    public PlayerStatsModel Stats { get; set; } = null!;
}