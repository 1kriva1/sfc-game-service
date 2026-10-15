using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Player;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Entities.Team.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Team;

namespace SFC.Game.Infrastructure.Persistence.Extensions.Include.Game;

public static class GamePlayerIncludeExtensions
{
    private static readonly Dictionary<string, GamePlayerIncludeOptions> IncludeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Player"] = GamePlayerIncludeOptions.Player,
        ["PlayerProfile"] = GamePlayerIncludeOptions.PlayerProfile,
        ["GameTeamPlayer"] = GamePlayerIncludeOptions.GameTeamPlayer,
        ["GameTeamPlayer.GameTeam"] = GamePlayerIncludeOptions.GameTeamPlayerWithGameTeam,
        ["GameTeamPlayer.GameTeam.Team"] = GamePlayerIncludeOptions.GameTeamPlayerWithGameTeamWithTeam,
        ["GameTeamPlayer.GameTeam.TeamProfile"] = GamePlayerIncludeOptions.GameTeamPlayerWithGameTeamWithTeamProfile,
        ["GameTeamPlayer.GameTeam.Team.TeamPlayers"] = GamePlayerIncludeOptions.GameTeamPlayerWithGameTeamWithTeamWithTeamPlayers,
        ["GameTeamPlayer.GameTeam.Team.TeamPlayers.Player"] = GamePlayerIncludeOptions.GameTeamPlayerWithGameTeamWithTeamWithTeamPlayersWithPlayer,
        ["GameTeamPlayer.GameTeam.Team.TeamPlayers.PlayerProfile"] = GamePlayerIncludeOptions.GameTeamPlayerWithGameTeamWithTeamWithTeamPlayersWithPlayerProfile
    };

    public static IQueryable<GamePlayer> IncludeGamePlayer(this IQueryable<GamePlayer> query, GamePlayerIncludeOptions options)
    {
        // player
        if (options.HasFlag(GamePlayerIncludeOptions.Player))
            query = query.Include(x => x.Player);

        if (options.HasFlag(GamePlayerIncludeOptions.PlayerProfile))
            query = query.IncludeNestedPlayerDetails(x => x.Player);

        // game team player
        if (options.HasFlag(GamePlayerIncludeOptions.GameTeamPlayer))
            query = query.IncludeGameTeamPlayerRoot();

        if (options.HasFlag(GamePlayerIncludeOptions.GameTeamPlayerWithGameTeam))
            query = query.IncludeGameTeamPlayerGameTeamRoot();

        if (options.HasFlag(GamePlayerIncludeOptions.GameTeamPlayerWithGameTeamWithTeam))
            query = query.IncludeGameTeamPlayerGameTeamTeamRoot();

        if (options.HasFlag(GamePlayerIncludeOptions.GameTeamPlayerWithGameTeamWithTeamProfile))
            query = query.IncludeTeamDetails(q => q.IncludeGameTeamPlayerGameTeamTeamRoot());

        if (options.HasFlag(GamePlayerIncludeOptions.GameTeamPlayerWithGameTeamWithTeamWithTeamPlayers))
            query = query.IncludeGameTeamPlayerGameTeamTeamTeamPlayersRoot();

        if (options.HasFlag(GamePlayerIncludeOptions.GameTeamPlayerWithGameTeamWithTeamWithTeamPlayersWithPlayer))
            query = query.IncludeGameTeamPlayerGameTeamTeamTeamPlayersPlayerRoot();

        if (options.HasFlag(GamePlayerIncludeOptions.GameTeamPlayerWithGameTeamWithTeamWithTeamPlayersWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGameTeamPlayerGameTeamTeamTeamPlayersPlayerRoot());

        return query;
    }

    public static IQueryable<GamePlayer> IncludeGamePlayer(this IQueryable<GamePlayer> query, params GamePlayerIncludeOptions[] options)
    {
        GamePlayerIncludeOptions merged = GamePlayerIncludeOptions.None;

        foreach (GamePlayerIncludeOptions option in options)
        {
            merged |= option;
        }

        return query.IncludeGamePlayer(merged);
    }

    public static IQueryable<GamePlayer> IncludeGamePlayer(this IQueryable<GamePlayer> query, IEnumerable<string>? includes, GamePlayerIncludeOptions defaultWhenEmpty = GamePlayerIncludeOptions.None)
    {
        GamePlayerIncludeOptions options = IncludeOptionsParser.Parse(includes, IncludeMap, defaultWhenEmpty);
        return query.IncludeGamePlayer(options);
    }

    #region Private

    private static IIncludableQueryable<GamePlayer, GameTeamPlayer> IncludeGameTeamPlayerRoot(this IQueryable<GamePlayer> gamePlayers)
        => gamePlayers.Include(x => x.GameTeamPlayer!);

    private static IIncludableQueryable<GamePlayer, GameTeam> IncludeGameTeamPlayerGameTeamRoot(this IQueryable<GamePlayer> gamePlayers)
        => gamePlayers.IncludeGameTeamPlayerRoot().ThenInclude(x => x.GameTeam);

    private static IIncludableQueryable<GamePlayer, TeamEntity> IncludeGameTeamPlayerGameTeamTeamRoot(this IQueryable<GamePlayer> gamePlayers)
        => gamePlayers.IncludeGameTeamPlayerGameTeamRoot().ThenInclude(x => x.Team);

    private static IIncludableQueryable<GamePlayer, ICollection<TeamPlayer>> IncludeGameTeamPlayerGameTeamTeamTeamPlayersRoot(this IQueryable<GamePlayer> gamePlayers)
        => gamePlayers.IncludeGameTeamPlayerGameTeamTeamRoot().ThenInclude(x => x.Players);

    private static IIncludableQueryable<GamePlayer, PlayerEntity> IncludeGameTeamPlayerGameTeamTeamTeamPlayersPlayerRoot(this IQueryable<GamePlayer> gamePlayers)
        => gamePlayers.IncludeGameTeamPlayerGameTeamTeamTeamPlayersRoot().ThenInclude(x => x.Player);

    #endregion Private
}