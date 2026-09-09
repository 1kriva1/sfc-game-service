namespace SFC.Game.Application.Features.Common.Constants;
public static class PlayerConstants
{
    public const int StatMaxValue = 100;

    public const int StarsMaxValue = 5;

    public static readonly Tuple<int, int> PlayerSizeRange = new(1, 300);

    public static readonly Tuple<int, int> StatValueRange = new(0, 100);
}