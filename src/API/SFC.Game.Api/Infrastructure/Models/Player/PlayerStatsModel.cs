using SFC.Game.Application.Common.Dto.Player.General;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Player;

/// <summary>
/// Player stats model.
/// </summary>
public class PlayerStatsModel : IMapFrom<PlayerStatsDto>
{
    /// <summary>
    /// Stats.
    /// </summary>
    public IEnumerable<PlayerStatValueModel> Values { get; set; } = [];
}