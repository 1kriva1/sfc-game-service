namespace SFC.Game.Application.Common.Dto.Player.General.Filters;
public class PlayerFilterDto
{
    public PlayerProfileFilterDto Profile { get; set; } = default!;

    public PlayerStatsFilterDto Stats { get; set; } = default!;
}