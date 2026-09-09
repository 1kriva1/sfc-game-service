namespace SFC.Game.Application.Common.Constants;
public static class ValidationConstants
{
    public const int NameValueMaxLength = 250;

    public const int DescriptionValueMaxLength = 1050;

    public const int TitleValueMaxLength = 150;

    public const int CityValueMaxLength = 100;

    public const int TagValueMaxLength = 20;

    public const int TagsMaxLength = 50;

    public const int FileMaxSizeInBytes = 5242880;

    public const int ExtensionValueMaxLength = 20;

    public const int PercentageMaxValue = 100;

    public static readonly Tuple<int, int> PlayerSizeRange = new(1, 300);

    public static readonly Tuple<int, int> RangeLimit = new(0, 100);
}