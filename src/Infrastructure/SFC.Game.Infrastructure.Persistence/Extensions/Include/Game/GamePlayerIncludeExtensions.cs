using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Entities.Team.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Team;

namespace SFC.Game.Infrastructure.Persistence.Extensions.Include.Game;

[Flags]
public enum GamePlayerIncludeOptions
{
    None = 0,
    Player = 1 << 0,
    PlayerProfile = 1 << 1,
    GameTeam = 1 << 2,
    GameTeamWithTeam = 1 << 3,
    GameTeamWithTeamProfile = 1 << 4,
    GameTeamWithTeamWithTeamPlayers = 1 << 5,
    GameTeamWithTeamWithTeamPlayersWithPlayer = 1 << 6,
    GameTeamWithTeamWithTeamPlayersWithPlayerProfile = 1 << 7,
    GameTeamWithGameTeamPlayers = 1 << 8,
    GameTeamWithGameTeamPlayersWithPlayer = 1 << 9,
    GameTeamWithGameTeamPlayersWithPlayerProfile = 1 << 10
}

public static class GamePlayerIncludeExtensions
{
    private static readonly Dictionary<string, GamePlayerIncludeOptions> IncludeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Player"] = GamePlayerIncludeOptions.Player,
        ["PlayerProfile"] = GamePlayerIncludeOptions.PlayerProfile,
        ["GameTeam"] = GamePlayerIncludeOptions.GameTeam,
        ["GameTeam.Team"] = GamePlayerIncludeOptions.GameTeamWithTeam,
        ["GameTeam.TeamProfile"] = GamePlayerIncludeOptions.GameTeamWithTeamProfile,
        ["GameTeam.Team.TeamPlayers"] = GamePlayerIncludeOptions.GameTeamWithTeamWithTeamPlayers,
        ["GameTeam.Team.TeamPlayers.Player"] = GamePlayerIncludeOptions.GameTeamWithTeamWithTeamPlayersWithPlayer,
        ["GameTeam.Team.TeamPlayers.PlayerProfile"] = GamePlayerIncludeOptions.GameTeamWithTeamWithTeamPlayersWithPlayerProfile,
        ["GameTeam.GameTeamPlayers"] = GamePlayerIncludeOptions.GameTeamWithGameTeamPlayers,
        ["GameTeam.GameTeamPlayers.Player"] = GamePlayerIncludeOptions.GameTeamWithGameTeamPlayersWithPlayer,
        ["GameTeam.GameTeamPlayers.PlayerProfile"] = GamePlayerIncludeOptions.GameTeamWithGameTeamPlayersWithPlayerProfile
    };

    public static IQueryable<GamePlayer> IncludeGamePlayer(this IQueryable<GamePlayer> query, GamePlayerIncludeOptions options)
    {
        // player
        if (options.HasFlag(GamePlayerIncludeOptions.Player))
            query = query.Include(x => x.Player);

        if (options.HasFlag(GamePlayerIncludeOptions.PlayerProfile))
            query = query.IncludeNestedPlayerDetails(x => x.Player);

        // game team
        if (options.HasFlag(GamePlayerIncludeOptions.GameTeam))
            query = query.IncludeGameTeamRoot();

        if (options.HasFlag(GamePlayerIncludeOptions.GameTeamWithTeam))
            query = query.IncludeGameTeamRoot().ThenInclude(x => x.Team);

        if (options.HasFlag(GamePlayerIncludeOptions.GameTeamWithTeamProfile))
            query = query.IncludeTeamDetails(q => q.IncludeGameTeamRoot().ThenInclude(x => x.Team));

        if (options.HasFlag(GamePlayerIncludeOptions.GameTeamWithTeamWithTeamPlayers))
            query = query.IncludeGameTeamTeamTeamPlayersRoot();

        if (options.HasFlag(GamePlayerIncludeOptions.GameTeamWithTeamWithTeamPlayersWithPlayer))
            query = query.IncludeGameTeamTeamTeamPlayersRoot().ThenInclude(x => x.Player);

        if (options.HasFlag(GamePlayerIncludeOptions.GameTeamWithTeamWithTeamPlayersWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGameTeamTeamTeamPlayersRoot().ThenInclude(x => x.Player));

        if (options.HasFlag(GamePlayerIncludeOptions.GameTeamWithGameTeamPlayers))
            query = query.IncludeGameTeamPlayersRoot();

        if (options.HasFlag(GamePlayerIncludeOptions.GameTeamWithGameTeamPlayersWithPlayer))
            query = query.IncludeGameTeamPlayersRoot().ThenInclude(x => x.Player);

        if (options.HasFlag(GamePlayerIncludeOptions.GameTeamWithGameTeamPlayersWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGameTeamPlayersRoot().ThenInclude(x => x.Player));

        return query;
    }

    public static IQueryable<GamePlayer> IncludeGamePlayer(this IQueryable<GamePlayer> query, GamePlayerIncludeOptions[] options)
    {
        foreach (GamePlayerIncludeOptions item in options)
        {
            query.IncludeGamePlayer(item);
        }

        return query;
    }

    public static IQueryable<GamePlayer> IncludeGamePlayer(this IQueryable<GamePlayer> query, IEnumerable<string>? includes, GamePlayerIncludeOptions defaultWhenEmpty = GamePlayerIncludeOptions.None)
    {
        GamePlayerIncludeOptions options = IncludeOptionsParser.Parse(includes, IncludeMap, defaultWhenEmpty);
        return query.IncludeGamePlayer(options);
    }

    #region Private

    private static IIncludableQueryable<GamePlayer, GameTeam> IncludeGameTeamRoot(this IQueryable<GamePlayer> gamePlayers)
        => gamePlayers.Include(x => x.GameTeam!);

    private static IIncludableQueryable<GamePlayer, ICollection<TeamPlayer>> IncludeGameTeamTeamTeamPlayersRoot(this IQueryable<GamePlayer> gamePlayers)
        => gamePlayers.IncludeGameTeamRoot().ThenInclude(x => x.Team).ThenInclude(x => x.Players);

    private static IIncludableQueryable<GamePlayer, ICollection<GameTeamPlayer>> IncludeGameTeamPlayersRoot(this IQueryable<GamePlayer> gamePlayers)
        => gamePlayers.IncludeGameTeamRoot().ThenInclude(x => x.Players);

    #endregion Private
}