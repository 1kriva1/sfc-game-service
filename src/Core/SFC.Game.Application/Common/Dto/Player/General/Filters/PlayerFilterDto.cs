namespace SFC.Game.Application.Common.Dto.Player.General.Filters;
public class PlayerFilterDto
{
    public IEnumerable<long>? ExcludeIds { get; set; }

    public PlayerProfileFilterDto Profile { get; set; } = default!;

    public PlayerStatsFilterDto Stats { get; set; } = default!;
}