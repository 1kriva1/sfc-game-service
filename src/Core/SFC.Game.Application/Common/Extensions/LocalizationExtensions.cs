using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Features.Game.Data.Queries.Common.Dto;

namespace SFC.Game.Application.Common.Extensions;

public static class LocalizationExtensions
{
    public static void Localize(this DataValueDto value) => value.Title = Localization.GetDataValue(value.Title);

    public static IEnumerable<T> Localize<T>(this IEnumerable<T> values)
        where T : DataValueDto
    {
        foreach (DataValueDto value in values)
        {
            value.Localize();
        }

        return values;
    }
}