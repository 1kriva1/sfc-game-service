namespace SFC.Game.Application.Common.Dto.Player.General.Filters;
public class PlayerProfileFilterDto
{
    public PlayerGeneralProfileFilterDto General { get; set; } = default!;

    public PlayerFootballProfileFilterDto Football { get; set; } = default!;
}