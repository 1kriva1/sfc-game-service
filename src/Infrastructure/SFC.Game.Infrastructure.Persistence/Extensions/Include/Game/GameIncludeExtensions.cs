using System.Linq.Expressions;

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
public enum GameIncludeOptions
{
    None = 0,
    Profile = 1 << 0,
    // game teams
    GameTeams = 1 << 1,
    GameTeamsWithTeam = 1 << 2,
    GameTeamsWithTeamProfile = 1 << 3,
    GameTeamsWithTeamWithTeamPlayers = 1 << 4,
    GameTeamsWithTeamWithTeamPlayersWithPlayer = 1 << 5,
    GameTeamsWithTeamWithTeamPlayersWithPlayerProfile = 1 << 6,
    GameTeamsWithGameTeamPlayers = 1 << 7,
    GameTeamsWithGameTeamPlayersWithPlayer = 1 << 8,
    GameTeamsWithGameTeamPlayersWithPlayerProfile = 1 << 9,
    // game players
    GamePlayers = 1 << 10,
    GamePlayersWithPlayer = 1 << 11,
    GamePlayersWithPlayerProfile = 1 << 12,
    GamePlayersWithGameTeam = 1 << 13,
    GamePlayersWithGameTeamWithTeam = 1 << 14,
    GamePlayersWithGameTeamWithTeamProfile = 1 << 15,
    GamePlayersWithGameTeamWithTeamWithTeamPlayers = 1 << 16,
    GamePlayersWithGameTeamWithTeamWithTeamPlayersWithPlayer = 1 << 17,
    GamePlayersWithGameTeamWithTeamWithTeamPlayersWithPlayerProfile = 1 << 18,
    GamePlayersWithGameTeamWithGameTeamPlayers = 1 << 19,
    GamePlayersWithGameTeamWithGameTeamPlayersWithPlayer = 1 << 20,
    GamePlayersWithGameTeamWithGameTeamPlayersWithPlayerProfile = 1 << 21
}

public static class GameIncludeExtensions
{
    private static readonly Dictionary<string, GameIncludeOptions> IncludeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Profile"] = GameIncludeOptions.Profile,
        ["GameTeams"] = GameIncludeOptions.GameTeams,
        ["GameTeams.Team"] = GameIncludeOptions.GameTeamsWithTeam,
        ["GameTeams.TeamProfile"] = GameIncludeOptions.GameTeamsWithTeamProfile,
        ["GameTeams.Team.TeamPlayers"] = GameIncludeOptions.GameTeamsWithTeamWithTeamPlayers,
        ["GameTeams.Team.TeamPlayers.Player"] = GameIncludeOptions.GameTeamsWithTeamWithTeamPlayersWithPlayer,
        ["GameTeams.Team.TeamPlayers.PlayerProfile"] = GameIncludeOptions.GameTeamsWithTeamWithTeamPlayersWithPlayerProfile,
        ["GameTeams.GameTeamPlayers"] = GameIncludeOptions.GameTeamsWithGameTeamPlayers,
        ["GameTeams.GameTeamPlayers.Player"] = GameIncludeOptions.GameTeamsWithGameTeamPlayersWithPlayer,
        ["GameTeams.GameTeamPlayers.PlayerProfile"] = GameIncludeOptions.GameTeamsWithGameTeamPlayersWithPlayerProfile,
        ["GamePlayers"] = GameIncludeOptions.GamePlayers,
        ["GamePlayers.Player"] = GameIncludeOptions.GamePlayersWithPlayer,
        ["GamePlayers.PlayerProfile"] = GameIncludeOptions.GamePlayersWithPlayerProfile,
        ["GamePlayers.GameTeam"] = GameIncludeOptions.GamePlayersWithGameTeam,
        ["GamePlayers.GameTeam.Team"] = GameIncludeOptions.GamePlayersWithGameTeamWithTeam,
        ["GamePlayers.GameTeam.TeamProfile"] = GameIncludeOptions.GamePlayersWithGameTeamWithTeamProfile,
        ["GamePlayers.GameTeam.Team.TeamPlayers"] = GameIncludeOptions.GamePlayersWithGameTeamWithTeamWithTeamPlayers,
        ["GamePlayers.GameTeam.Team.TeamPlayers.Player"] = GameIncludeOptions.GamePlayersWithGameTeamWithTeamWithTeamPlayersWithPlayer,
        ["GamePlayers.GameTeam.Team.TeamPlayers.PlayerProfile"] = GameIncludeOptions.GamePlayersWithGameTeamWithTeamWithTeamPlayersWithPlayerProfile,
        ["GamePlayers.GameTeam.GameTeamPlayers"] = GameIncludeOptions.GamePlayersWithGameTeamWithGameTeamPlayers,
        ["GamePlayers.GameTeam.GameTeamPlayers.Player"] = GameIncludeOptions.GamePlayersWithGameTeamWithGameTeamPlayersWithPlayer,
        ["GamePlayers.GameTeam.GameTeamPlayers.PlayerProfile"] = GameIncludeOptions.GamePlayersWithGameTeamWithGameTeamPlayersWithPlayerProfile
    };

    public static IQueryable<GameEntity> IncludeGame(this IQueryable<GameEntity> query, GameIncludeOptions options)
    {
        if (options.HasFlag(GameIncludeOptions.Profile))
            query = query.IncludeGameProfile();

        // teams
        if (options.HasFlag(GameIncludeOptions.GameTeams))
            query = query.IncludeGameTeamsRoot();

        if (options.HasFlag(GameIncludeOptions.GameTeamsWithTeam))
            query = query.IncludeGameTeamsRoot().ThenInclude(x => x.Team);

        if (options.HasFlag(GameIncludeOptions.GameTeamsWithTeamProfile))
            query = query.IncludeTeamDetails(q => q.IncludeGameTeamsRoot().ThenInclude(x => x.Team));

        if (options.HasFlag(GameIncludeOptions.GameTeamsWithTeamWithTeamPlayers))
            query = query.IncludeGameTeamsTeamTeamPlayersRoot();

        if (options.HasFlag(GameIncludeOptions.GameTeamsWithTeamWithTeamPlayersWithPlayer))
            query = query.IncludeGameTeamsTeamTeamPlayersRoot().ThenInclude(x => x.Player);

        if (options.HasFlag(GameIncludeOptions.GameTeamsWithTeamWithTeamPlayersWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGameTeamsTeamTeamPlayersRoot().ThenInclude(x => x.Player));

        if (options.HasFlag(GameIncludeOptions.GameTeamsWithGameTeamPlayers))
            query = query.IncludeGameTeamsGameTeamPlayerRoot();

        if (options.HasFlag(GameIncludeOptions.GameTeamsWithGameTeamPlayersWithPlayer))
            query = query.IncludeGameTeamsGameTeamPlayerRoot().ThenInclude(x => x.Player);

        if (options.HasFlag(GameIncludeOptions.GameTeamsWithGameTeamPlayersWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGameTeamsGameTeamPlayerRoot().ThenInclude(x => x.Player));

        //players
        if (options.HasFlag(GameIncludeOptions.GamePlayers))
            query = query.IncludeGamePlayersRoot();

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithPlayer))
            query = query.IncludeGamePlayersRoot().ThenInclude(x => x.Player);

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGamePlayersRoot().ThenInclude(x => x.Player));

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeam))
            query = query.IncludeGamePlayersGameTeamRoot();

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeamWithTeam))
            query = query.IncludeGamePlayersGameTeamRoot().ThenInclude(x => x.Team);

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeamWithTeamProfile))
            query = query.IncludeTeamDetails(q => q.IncludeGamePlayersGameTeamRoot().ThenInclude(x => x.Team));

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeamWithTeamWithTeamPlayers))
            query = query.IncludeGamePlayersGameTeamTeamTeamPlayersRoot();

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeamWithTeamWithTeamPlayersWithPlayer))
            query = query.IncludeGamePlayersGameTeamTeamTeamPlayersRoot().ThenInclude(x => x.Player);

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeamWithTeamWithTeamPlayersWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGamePlayersGameTeamTeamTeamPlayersRoot().ThenInclude(x => x.Player));

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeamWithGameTeamPlayers))
            query = query.IncludeGamePlayersGameTeamPlayersRoot();

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeamWithGameTeamPlayersWithPlayer))
            query = query.IncludeGamePlayersGameTeamPlayersRoot().ThenInclude(x => x.Player);

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeamWithGameTeamPlayersWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGamePlayersGameTeamPlayersRoot().ThenInclude(x => x.Player));

        return query;
    }

    public static IQueryable<GameEntity> IncludeGame(this IQueryable<GameEntity> query, GameIncludeOptions[] options)
    {
        foreach (GameIncludeOptions item in options)
        {
            query.IncludeGame(item);
        }

        return query;
    }

    public static IQueryable<GameEntity> IncludeGame(this IQueryable<GameEntity> query, IEnumerable<string>? includes, GameIncludeOptions defaultWhenEmpty = GameIncludeOptions.None)
    {
        GameIncludeOptions options = IncludeOptionsParser.Parse(includes, IncludeMap, defaultWhenEmpty);
        return query.IncludeGame(options);
    }

    public static IQueryable<TSource> IncludeGameDetails<TSource>(
    this IQueryable<TSource> source,
    Func<IQueryable<TSource>, IIncludableQueryable<TSource, GameEntity>> includeGame)
    where TSource : class
    {
        return includeGame(source)
            .ThenInclude(p => p.GeneralProfile)
            .IncludeFromGame(includeGame, p => p.InventaryProfile)
            .IncludeFromGame(includeGame, p => p.FinancialProfile)
            .IncludeFromGame(includeGame, p => p.Availability)
            .IncludeFromGame(includeGame, p => p.Tags);
    }

    public static IQueryable<TSource> IncludeNestedGameDetails<TSource>(
    this IQueryable<TSource> source,
    Expression<Func<TSource, GameEntity>> gameNavigation)
    where TSource : class
    {
        return source.Include(gameNavigation)
            .ThenInclude(p => p.GeneralProfile)
            .IncludeFromNestedGame(gameNavigation, p => p.InventaryProfile)
            .IncludeFromNestedGame(gameNavigation, p => p.FinancialProfile)
            .IncludeFromNestedGame(gameNavigation, p => p.Availability)
            .IncludeFromNestedGame(gameNavigation, p => p.Tags);
    }

    #region Private

    private static IQueryable<GameEntity> IncludeGameProfile(this IQueryable<GameEntity> games)
    {
        return games
            .Include(p => p.GeneralProfile)
            .Include(p => p.InventaryProfile)
            .Include(p => p.FinancialProfile)
            .Include(p => p.Availability)
            .Include(p => p.Tags);
    }

    private static IQueryable<TSource> IncludeFromGame<TSource, TProperty>(
    this IQueryable<TSource> source,
    Func<IQueryable<TSource>, IIncludableQueryable<TSource, GameEntity>> includeGame,
    Expression<Func<GameEntity, TProperty>> navigation)
    where TSource : class
    {
        return includeGame(source).ThenInclude(navigation);
    }

    private static IQueryable<TSource> IncludeFromNestedGame<TSource, TProperty>(
        this IQueryable<TSource> source,
        Expression<Func<TSource, GameEntity>> gameNavigation,
        Expression<Func<GameEntity, TProperty>> navigation)
        where TSource : class
    {
        return source.Include(gameNavigation).ThenInclude(navigation);
    }

    private static IIncludableQueryable<GameEntity, ICollection<GameTeam>> IncludeGameTeamsRoot(this IQueryable<GameEntity> games)
        => games.Include(p => p.Teams);

    private static IIncludableQueryable<GameEntity, ICollection<TeamPlayer>> IncludeGameTeamsTeamTeamPlayersRoot(this IQueryable<GameEntity> games)
        => games.IncludeGameTeamsRoot().ThenInclude(x => x.Team).ThenInclude(x => x.Players);

    private static IIncludableQueryable<GameEntity, ICollection<GameTeamPlayer>> IncludeGameTeamsGameTeamPlayerRoot(this IQueryable<GameEntity> games)
        => games.IncludeGameTeamsRoot().ThenInclude(x => x.Players);

    private static IIncludableQueryable<GameEntity, ICollection<GamePlayer>> IncludeGamePlayersRoot(this IQueryable<GameEntity> games)
        => games.Include(p => p.Players);

    private static IIncludableQueryable<GameEntity, GameTeam> IncludeGamePlayersGameTeamRoot(this IQueryable<GameEntity> games)
        => games.IncludeGamePlayersRoot().ThenInclude(x => x.GameTeam!);

    private static IIncludableQueryable<GameEntity, ICollection<GameTeamPlayer>> IncludeGamePlayersGameTeamPlayersRoot(this IQueryable<GameEntity> games)
        => games.IncludeGamePlayersRoot().ThenInclude(x => x.GameTeam!).ThenInclude(x => x.Players);

    private static IIncludableQueryable<GameEntity, ICollection<TeamPlayer>> IncludeGamePlayersGameTeamTeamTeamPlayersRoot(this IQueryable<GameEntity> games)
        => games.IncludeGamePlayersRoot().ThenInclude(x => x.GameTeam!).ThenInclude(x => x.Team).ThenInclude(x => x.Players);

    #endregion Private
}