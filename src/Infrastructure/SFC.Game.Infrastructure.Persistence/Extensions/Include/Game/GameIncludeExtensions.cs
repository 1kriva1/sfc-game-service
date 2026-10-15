using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Entities.Team.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Team;

namespace SFC.Game.Infrastructure.Persistence.Extensions.Include.Game;

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
        ["GameTeams.GameTeamPlayers.GamePlayer"] = GameIncludeOptions.GameTeamsWithGameTeamPlayersWithGamePlayer,
        ["GameTeams.GameTeamPlayers.GamePlayer.Player"] = GameIncludeOptions.GameTeamsWithGameTeamPlayersWithGamePlayerWithPlayer,
        ["GameTeams.GameTeamPlayers.GamePlayer.PlayerProfile"] = GameIncludeOptions.GameTeamsWithGameTeamPlayersWithGamePlayerWithPlayerProfile,
        ["GameTeams.GameTeamPlayers.TeamPlayer"] = GameIncludeOptions.GameTeamsWithGameTeamPlayersWithTeamPlayer,
        ["GameTeams.GameTeamPlayers.TeamPlayer.Player"] = GameIncludeOptions.GameTeamsWithGameTeamPlayersWithTeamPlayerWithPlayer,
        ["GameTeams.GameTeamPlayers.TeamPlayer.PlayerProfile"] = GameIncludeOptions.GameTeamsWithGameTeamPlayersWithTeamPlayerWithPlayerProfile,
        ["GamePlayers"] = GameIncludeOptions.GamePlayers,
        ["GamePlayers.Player"] = GameIncludeOptions.GamePlayersWithPlayer,
        ["GamePlayers.PlayerProfile"] = GameIncludeOptions.GamePlayersWithPlayerProfile,
        ["GamePlayers.GameTeamPlayer"] = GameIncludeOptions.GamePlayersWithGameTeamPlayer,
        ["GamePlayers.GameTeamPlayer.GameTeam"] = GameIncludeOptions.GamePlayersWithGameTeamPlayerWithGameTeam,
        ["GamePlayers.GameTeamPlayer.GameTeam.Team"] = GameIncludeOptions.GamePlayersWithGameTeamPlayerWithGameTeamWithTeam,
        ["GamePlayers.GameTeamPlayer.GameTeam.TeamProfile"] = GameIncludeOptions.GamePlayersWithGameTeamPlayerWithGameTeamWithTeamProfile,
        ["GamePlayers.GameTeamPlayer.GameTeam.Team.TeamPlayers"] = GameIncludeOptions.GamePlayersWithGameTeamPlayerWithGameTeamWithTeamWithTeamPlayers,
        ["GamePlayers.GameTeamPlayer.GameTeam.Team.TeamPlayers.Player"] = GameIncludeOptions.GamePlayersWithGameTeamPlayerWithGameTeamWithTeamWithTeamPlayersWithPlayer,
        ["GamePlayers.GameTeamPlayer.GameTeam.Team.TeamPlayers.PlayerProfile"] = GameIncludeOptions.GamePlayersWithGameTeamPlayerWithGameTeamWithTeamWithTeamPlayersWithPlayerProfile
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

        if (options.HasFlag(GameIncludeOptions.GameTeamsWithGameTeamPlayersWithGamePlayer))
            query = query.IncludeGameTeamsGameTeamPlayerGamePlayerGamePlayerRoot();

        if (options.HasFlag(GameIncludeOptions.GameTeamsWithGameTeamPlayersWithGamePlayerWithPlayer))
            query = query.IncludeGameTeamsGameTeamPlayerGamePlayerGamePlayerPlayerRoot();

        if (options.HasFlag(GameIncludeOptions.GameTeamsWithGameTeamPlayersWithGamePlayerWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGameTeamsGameTeamPlayerGamePlayerGamePlayerPlayerRoot()!);

        if (options.HasFlag(GameIncludeOptions.GameTeamsWithGameTeamPlayersWithTeamPlayer))
            query = query.IncludeGameTeamsGameTeamPlayerGamePlayerTeamPlayerRoot();

        if (options.HasFlag(GameIncludeOptions.GameTeamsWithGameTeamPlayersWithTeamPlayerWithPlayer))
            query = query.IncludeGameTeamsGameTeamPlayerGamePlayerTeamPlayerPlayerRoot();

        if (options.HasFlag(GameIncludeOptions.GameTeamsWithGameTeamPlayersWithTeamPlayerWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGameTeamsGameTeamPlayerGamePlayerTeamPlayerPlayerRoot()!);

        //players
        if (options.HasFlag(GameIncludeOptions.GamePlayers))
            query = query.IncludeGamePlayersRoot();

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithPlayer))
            query = query.IncludeGamePlayersRoot().ThenInclude(x => x.Player);

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGamePlayersRoot().ThenInclude(x => x.Player));

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeamPlayer))
            query = query.IncludeGamePlayersGameTeamPlayerRoot();

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeamPlayerWithGameTeam))
            query = query.IncludeGamePlayersGameTeamPlayerGameTeamRoot();

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeamPlayerWithGameTeamWithTeam))
            query = query.IncludeGamePlayersGameTeamPlayerGameTeamTeamRoot();

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeamPlayerWithGameTeamWithTeamProfile))
            query = query.IncludeTeamDetails(q => q.IncludeGamePlayersGameTeamPlayerGameTeamTeamRoot());

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeamPlayerWithGameTeamWithTeamWithTeamPlayers))
            query = query.IncludeGamePlayersGameTeamPlayerGameTeamTeamTeamPlayersRoot();

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeamPlayerWithGameTeamWithTeamWithTeamPlayersWithPlayer))
            query = query.IncludeGamePlayersGameTeamPlayerGameTeamTeamTeamPlayersPlayerRoot();

        if (options.HasFlag(GameIncludeOptions.GamePlayersWithGameTeamPlayerWithGameTeamWithTeamWithTeamPlayersWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGamePlayersGameTeamPlayerGameTeamTeamTeamPlayersPlayerRoot());

        return query;
    }

    public static IQueryable<GameEntity> IncludeGame(this IQueryable<GameEntity> query, params GameIncludeOptions[] options)
    {
        GameIncludeOptions merged = GameIncludeOptions.None;

        foreach (GameIncludeOptions option in options)
        {
            merged |= option;
        }

        return query.IncludeGame(merged);
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

    private static IIncludableQueryable<GameEntity, GamePlayer?> IncludeGameTeamsGameTeamPlayerGamePlayerGamePlayerRoot(this IQueryable<GameEntity> games)
        => games.IncludeGameTeamsGameTeamPlayerRoot().ThenInclude(x => x.GamePlayer);

    private static IIncludableQueryable<GameEntity, PlayerEntity?> IncludeGameTeamsGameTeamPlayerGamePlayerGamePlayerPlayerRoot(this IQueryable<GameEntity> games)
        => games.IncludeGameTeamsGameTeamPlayerGamePlayerGamePlayerRoot().ThenInclude(x => x!.Player);

    private static IIncludableQueryable<GameEntity, TeamPlayer?> IncludeGameTeamsGameTeamPlayerGamePlayerTeamPlayerRoot(this IQueryable<GameEntity> games)
        => games.IncludeGameTeamsGameTeamPlayerRoot().ThenInclude(x => x.TeamPlayer);

    private static IIncludableQueryable<GameEntity, PlayerEntity?> IncludeGameTeamsGameTeamPlayerGamePlayerTeamPlayerPlayerRoot(this IQueryable<GameEntity> games)
       => games.IncludeGameTeamsGameTeamPlayerGamePlayerTeamPlayerRoot().ThenInclude(x => x!.Player);

    private static IIncludableQueryable<GameEntity, ICollection<GamePlayer>> IncludeGamePlayersRoot(this IQueryable<GameEntity> games)
        => games.Include(p => p.Players);

    private static IIncludableQueryable<GameEntity, GameTeamPlayer> IncludeGamePlayersGameTeamPlayerRoot(this IQueryable<GameEntity> games)
        => games.IncludeGamePlayersRoot().ThenInclude(x => x.GameTeamPlayer!);

    private static IIncludableQueryable<GameEntity, GameTeam> IncludeGamePlayersGameTeamPlayerGameTeamRoot(this IQueryable<GameEntity> games)
        => games.IncludeGamePlayersGameTeamPlayerRoot().ThenInclude(x => x.GameTeam!);

    private static IIncludableQueryable<GameEntity, TeamEntity> IncludeGamePlayersGameTeamPlayerGameTeamTeamRoot(this IQueryable<GameEntity> games)
        => games.IncludeGamePlayersGameTeamPlayerGameTeamRoot().ThenInclude(x => x.Team);

    private static IIncludableQueryable<GameEntity, ICollection<TeamPlayer>> IncludeGamePlayersGameTeamPlayerGameTeamTeamTeamPlayersRoot(this IQueryable<GameEntity> games)
        => games.IncludeGamePlayersGameTeamPlayerGameTeamTeamRoot().ThenInclude(x => x.Players);

    private static IIncludableQueryable<GameEntity, PlayerEntity> IncludeGamePlayersGameTeamPlayerGameTeamTeamTeamPlayersPlayerRoot(this IQueryable<GameEntity> games)
        => games.IncludeGamePlayersGameTeamPlayerGameTeamTeamTeamPlayersRoot().ThenInclude(x => x.Player);

    #endregion Private
}