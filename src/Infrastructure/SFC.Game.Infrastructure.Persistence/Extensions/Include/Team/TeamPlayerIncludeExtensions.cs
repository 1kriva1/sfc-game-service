using Microsoft.EntityFrameworkCore;

using SFC.Game.Domain.Entities.Team.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Player;

namespace SFC.Game.Infrastructure.Persistence.Extensions.Include.Team;

[Flags]
public enum TeamPlayerIncludeOptions
{
    None = 0,
    Player = 1 << 0,
    PlayerProfile = 1 << 1,
}

public static class TeamPlayerIncludeExtensions
{
    private static readonly Dictionary<string, TeamPlayerIncludeOptions> IncludeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Player"] = TeamPlayerIncludeOptions.Player,
        ["PlayerProfile"] = TeamPlayerIncludeOptions.PlayerProfile
    };

    public static IQueryable<TeamPlayer> IncludeTeamPlayer(this IQueryable<TeamPlayer> query, TeamPlayerIncludeOptions options)
    {
        // player
        if (options.HasFlag(TeamPlayerIncludeOptions.Player))
            query = query.Include(x => x.Player);

        if (options.HasFlag(TeamPlayerIncludeOptions.PlayerProfile))
            query = query.IncludeNestedPlayerDetails(x => x.Player);

        return query;
    }

    public static IQueryable<TeamPlayer> IncludeTeamPlayer(this IQueryable<TeamPlayer> query, TeamPlayerIncludeOptions[] options)
    {
        foreach (TeamPlayerIncludeOptions item in options)
        {
            query.IncludeTeamPlayer(item);
        }

        return query;
    }

    public static IQueryable<TeamPlayer> IncludeTeamPlayer(this IQueryable<TeamPlayer> query, IEnumerable<string>? includes, TeamPlayerIncludeOptions defaultWhenEmpty = TeamPlayerIncludeOptions.None)
    {
        TeamPlayerIncludeOptions options = IncludeOptionsParser.Parse(includes, IncludeMap, defaultWhenEmpty);
        return query.IncludeTeamPlayer(options);
    }
}