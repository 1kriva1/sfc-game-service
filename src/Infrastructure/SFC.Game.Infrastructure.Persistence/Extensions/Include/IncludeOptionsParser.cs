using System.Globalization;

namespace SFC.Game.Infrastructure.Persistence.Extensions.Include;

public static class IncludeOptionsParser
{
    public static TOptions Parse<TOptions>(
        IEnumerable<string>? includes,
        IReadOnlyDictionary<string, TOptions> map,
        TOptions defaultWhenEmpty)
        where TOptions : struct, Enum
    {
        if (includes is null)
        {
            return defaultWhenEmpty;
        }

        ulong result = 0UL;

        foreach (string token in includes.SelectMany(x =>
                     x.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)))
        {
            if (map.TryGetValue(token, out TOptions flag))
            {
                result |= Convert.ToUInt64(flag, CultureInfo.InvariantCulture);
            }
        }

        return result == 0UL
            ? defaultWhenEmpty
            : (TOptions)Enum.ToObject(typeof(TOptions), result);
    }
}