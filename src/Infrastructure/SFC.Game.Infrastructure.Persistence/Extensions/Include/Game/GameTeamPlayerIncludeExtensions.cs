using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Entities.Team.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Team;

namespace SFC.Game.Infrastructure.Persistence.Extensions.Include.Game;

[Flags]
public enum GameTeamPlayerIncludeOptions
{
    None = 0,
    Player = 1 << 1,
    PlayerProfile = 1 << 2,
    GameTeam = 1 << 3,
    GameTeamWithTeam = 1 << 4,
    GameTeamWithTeamProfile = 1 << 5,
    GameTeamWithTeamWithTeamPlayers = 1 << 6,
    GameTeamWithTeamWithTeamPlayersWithPlayer = 1 << 7,
    GameTeamWithTeamWithTeamPlayersWithPlayerProfile = 1 << 8,
    GameTeamWithGameTeamPlayers = 1 << 9,
    GameTeamWithGameTeamPlayersWithPlayer = 1 << 10,
    GameTeamWithGameTeamPlayersWithPlayerProfile = 1 << 11
}

public static class GameTeamPlayerIncludeExtensions
{
    private static readonly Dictionary<string, GameTeamPlayerIncludeOptions> IncludeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Player"] = GameTeamPlayerIncludeOptions.Player,
        ["PlayerProfile"] = GameTeamPlayerIncludeOptions.PlayerProfile,
        ["GameTeam"] = GameTeamPlayerIncludeOptions.GameTeam,
        ["GameTeam.Team"] = GameTeamPlayerIncludeOptions.GameTeamWithTeam,
        ["GameTeam.TeamProfile"] = GameTeamPlayerIncludeOptions.GameTeamWithTeamProfile,
        ["GameTeam.Team.TeamPlayers"] = GameTeamPlayerIncludeOptions.GameTeamWithTeamWithTeamPlayers,
        ["GameTeam.Team.TeamPlayers.Player"] = GameTeamPlayerIncludeOptions.GameTeamWithTeamWithTeamPlayersWithPlayer,
        ["GameTeam.Team.TeamPlayers.PlayerProfile"] = GameTeamPlayerIncludeOptions.GameTeamWithTeamWithTeamPlayersWithPlayerProfile,
        ["GameTeam.GameTeamPlayers"] = GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayers,
        ["GameTeam.GameTeamPlayers.Player"] = GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayersWithPlayer,
        ["GameTeam.GameTeamPlayers.PlayerProfile"] = GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayersWithPlayerProfile,
    };

    public static IQueryable<GameTeamPlayer> IncludeGameTeamPlayer(this IQueryable<GameTeamPlayer> query, GameTeamPlayerIncludeOptions options)
    {
        // player
        if (options.HasFlag(GameTeamPlayerIncludeOptions.Player))
            query = query.IncludePlayerRoot();

        if (options.HasFlag(GameTeamPlayerIncludeOptions.PlayerProfile))
            query = query.IncludePlayerDetails(q => q.Include(x => x.Player));

        // game team
        if (options.HasFlag(GameTeamPlayerIncludeOptions.GameTeam))
            query = query.IncludeGameTeamRoot();

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GameTeamWithTeam))
            query = query.IncludeGameTeamRoot().ThenInclude(x => x.Team);

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GameTeamWithTeamProfile))
            query = query.IncludeTeamDetails(q => q.IncludeGameTeamRoot().ThenInclude(x => x.Team));

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GameTeamWithTeamWithTeamPlayers))
            query = query.IncludeGameTeamTeamTeamPlayersRoot();

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GameTeamWithTeamWithTeamPlayersWithPlayer))
            query = query.IncludeGameTeamTeamTeamPlayersRoot().ThenInclude(q => q.Player);

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GameTeamWithTeamWithTeamPlayersWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGameTeamTeamTeamPlayersRoot().ThenInclude(x => x.Player));

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayers))
            query = query.IncludeGameTeamGameTeamPlayersRoot();

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayersWithPlayer))
            query = query.IncludeGameTeamGameTeamPlayersRoot().ThenInclude(x => x.Player);

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayersWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGameTeamGameTeamPlayersRoot().ThenInclude(x => x.Player));

        return query;
    }

    public static IQueryable<GameTeamPlayer> IncludeGameTeamPlayer(this IQueryable<GameTeamPlayer> query, params GameTeamPlayerIncludeOptions[] options)
    {
        foreach (GameTeamPlayerIncludeOptions option in options)
        {
            query.IncludeGameTeamPlayer(option);
        }

        return query;
    }

    public static IQueryable<GameTeamPlayer> IncludeGameTeamPlayer(this IQueryable<GameTeamPlayer> query, IEnumerable<string>? includes, GameTeamPlayerIncludeOptions defaultWhenEmpty = GameTeamPlayerIncludeOptions.None)
    {
        GameTeamPlayerIncludeOptions options = IncludeOptionsParser.Parse(includes, IncludeMap, defaultWhenEmpty);
        return query.IncludeGameTeamPlayer(options);
    }

    #region Private

    private static IIncludableQueryable<GameTeamPlayer, PlayerEntity> IncludePlayerRoot(this IQueryable<GameTeamPlayer> gameTeamPlayers)
        => gameTeamPlayers.Include(x => x.Player);

    private static IIncludableQueryable<GameTeamPlayer, GameTeam> IncludeGameTeamRoot(this IQueryable<GameTeamPlayer> gameTeamPlayers)
        => gameTeamPlayers.Include(p => p.GameTeam);

    private static IIncludableQueryable<GameTeamPlayer, ICollection<TeamPlayer>> IncludeGameTeamTeamTeamPlayersRoot(this IQueryable<GameTeamPlayer> gameTeamPlayers)
        => gameTeamPlayers.IncludeGameTeamRoot().ThenInclude(x => x.Team).ThenInclude(x => x.Players);

    private static IIncludableQueryable<GameTeamPlayer, ICollection<GameTeamPlayer>> IncludeGameTeamGameTeamPlayersRoot(this IQueryable<GameTeamPlayer> gameTeamPlayers)
        => gameTeamPlayers.IncludeGameTeamRoot().ThenInclude(x => x.Players);

    #endregion Private
}