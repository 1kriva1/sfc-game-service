using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team.Player;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Entities.Team.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Team;

namespace SFC.Game.Infrastructure.Persistence.Extensions.Include.Game;

public static class GameTeamPlayerIncludeExtensions
{
    private static readonly Dictionary<string, GameTeamPlayerIncludeOptions> IncludeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["All"] = GameTeamPlayerIncludeOptions.All,
        ["GamePlayer"] = GameTeamPlayerIncludeOptions.GamePlayer,
        ["GamePlayer.Player"] = GameTeamPlayerIncludeOptions.GamePlayerWithPlayer,
        ["GamePlayer.PlayerProfile"] = GameTeamPlayerIncludeOptions.GamePlayerWithPlayerProfile,
        ["TeamPlayer"] = GameTeamPlayerIncludeOptions.TeamPlayer,
        ["TeamPlayer.Player"] = GameTeamPlayerIncludeOptions.TeamPlayerWithPlayer,
        ["TeamPlayer.PlayerProfile"] = GameTeamPlayerIncludeOptions.TeamPlayerWithPlayerProfile,
        ["GameTeam"] = GameTeamPlayerIncludeOptions.GameTeam,
        ["GameTeam.Team"] = GameTeamPlayerIncludeOptions.GameTeamWithTeam,
        ["GameTeam.TeamProfile"] = GameTeamPlayerIncludeOptions.GameTeamWithTeamProfile,
        ["GameTeam.Team.TeamPlayers"] = GameTeamPlayerIncludeOptions.GameTeamWithTeamWithTeamPlayers,
        ["GameTeam.Team.TeamPlayers.Player"] = GameTeamPlayerIncludeOptions.GameTeamWithTeamWithTeamPlayersWithPlayer,
        ["GameTeam.Team.TeamPlayers.PlayerProfile"] = GameTeamPlayerIncludeOptions.GameTeamWithTeamWithTeamPlayersWithPlayerProfile,
        ["GameTeam.GameTeamPlayers"] = GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayers,
        ["GameTeam.GameTeamPlayers.GamePlayer"] = GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayersWithGamePlayer,
        ["GameTeam.GameTeamPlayers.GamePlayer.Player"] = GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayersWithGamePlayerWithPlayer,
        ["GameTeam.GameTeamPlayers.GamePlayer.PlayerProfile"] = GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayersWithGamePlayerWithPlayerProfile,
    };

    public static IQueryable<GameTeamPlayer> IncludeGameTeamPlayer(this IQueryable<GameTeamPlayer> query, GameTeamPlayerIncludeOptions options)
    {
        // game player
        if (options.HasFlag(GameTeamPlayerIncludeOptions.GamePlayer))
            query = query.IncludeGamePlayerRoot();

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GamePlayerWithPlayer))
            query = query.IncludeGamePlayerPlayerRoot();

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GamePlayerWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGamePlayerPlayerRoot()!);

        // team player
        if (options.HasFlag(GameTeamPlayerIncludeOptions.TeamPlayer))
            query = query.IncludeTeamPlayerRoot();

        if (options.HasFlag(GameTeamPlayerIncludeOptions.TeamPlayerWithPlayer))
            query = query.IncludeTeamPlayerPlayerRoot();

        if (options.HasFlag(GameTeamPlayerIncludeOptions.TeamPlayerWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeTeamPlayerPlayerRoot()!);

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

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayersWithGamePlayer))
            query = query.IncludeGameTeamGameTeamPlayersGamePlayerPlayerRoot();

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayersWithGamePlayerWithPlayer))
            query = query.IncludeGameTeamGameTeamPlayersGamePlayerPlayerRoot();

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayersWithGamePlayerWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGameTeamGameTeamPlayersGamePlayerPlayerRoot()!);

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayersWithTeamPlayer))
            query = query.IncludeGameTeamGameTeamPlayersTeamPlayerPlayerRoot();

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayersWithTeamPlayerWithPlayer))
            query = query.IncludeGameTeamGameTeamPlayersTeamPlayerPlayerRoot();

        if (options.HasFlag(GameTeamPlayerIncludeOptions.GameTeamWithGameTeamPlayersWithTeamPlayerWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGameTeamGameTeamPlayersTeamPlayerPlayerRoot()!);

        return query;
    }

    public static IQueryable<GameTeamPlayer> IncludeGameTeamPlayer(this IQueryable<GameTeamPlayer> query, params GameTeamPlayerIncludeOptions[] options)
    {
        GameTeamPlayerIncludeOptions merged = GameTeamPlayerIncludeOptions.None;

        foreach (GameTeamPlayerIncludeOptions option in options)
        {
            merged |= option;
        }

        return query.IncludeGameTeamPlayer(merged);
    }

    public static IQueryable<GameTeamPlayer> IncludeGameTeamPlayer(this IQueryable<GameTeamPlayer> query, IEnumerable<string>? includes, GameTeamPlayerIncludeOptions defaultWhenEmpty = GameTeamPlayerIncludeOptions.None)
    {
        GameTeamPlayerIncludeOptions options = IncludeOptionsParser.Parse(includes, IncludeMap, defaultWhenEmpty);
        return query.IncludeGameTeamPlayer(options);
    }

    #region Private

    private static IIncludableQueryable<GameTeamPlayer, GamePlayer?> IncludeGamePlayerRoot(this IQueryable<GameTeamPlayer> gameTeamPlayers)
        => gameTeamPlayers.Include(x => x.GamePlayer);

    private static IIncludableQueryable<GameTeamPlayer, PlayerEntity?> IncludeGamePlayerPlayerRoot(this IQueryable<GameTeamPlayer> gameTeamPlayers)
        => gameTeamPlayers.IncludeGamePlayerRoot().ThenInclude(x => x!.Player);

    private static IIncludableQueryable<GameTeamPlayer, TeamPlayer?> IncludeTeamPlayerRoot(this IQueryable<GameTeamPlayer> gameTeamPlayers)
        => gameTeamPlayers.Include(x => x.TeamPlayer);

    private static IIncludableQueryable<GameTeamPlayer, PlayerEntity?> IncludeTeamPlayerPlayerRoot(this IQueryable<GameTeamPlayer> gameTeamPlayers)
       => gameTeamPlayers.IncludeTeamPlayerRoot().ThenInclude(x => x!.Player);

    private static IIncludableQueryable<GameTeamPlayer, GameTeam> IncludeGameTeamRoot(this IQueryable<GameTeamPlayer> gameTeamPlayers)
        => gameTeamPlayers.Include(p => p.GameTeam);

    private static IIncludableQueryable<GameTeamPlayer, ICollection<TeamPlayer>> IncludeGameTeamTeamTeamPlayersRoot(this IQueryable<GameTeamPlayer> gameTeamPlayers)
        => gameTeamPlayers.IncludeGameTeamRoot().ThenInclude(x => x.Team).ThenInclude(x => x.Players);

    private static IIncludableQueryable<GameTeamPlayer, ICollection<GameTeamPlayer>> IncludeGameTeamGameTeamPlayersRoot(this IQueryable<GameTeamPlayer> gameTeamPlayers)
        => gameTeamPlayers.IncludeGameTeamRoot().ThenInclude(x => x.Players);

    private static IIncludableQueryable<GameTeamPlayer, GamePlayer?> IncludeGameTeamGameTeamPlayersGamePlayerRoot(this IQueryable<GameTeamPlayer> gameTeamPlayers)
        => gameTeamPlayers.IncludeGameTeamGameTeamPlayersRoot().ThenInclude(x => x.GamePlayer);

    private static IIncludableQueryable<GameTeamPlayer, PlayerEntity?> IncludeGameTeamGameTeamPlayersGamePlayerPlayerRoot(this IQueryable<GameTeamPlayer> gameTeamPlayers)
        => gameTeamPlayers.IncludeGameTeamGameTeamPlayersGamePlayerRoot().ThenInclude(x => x!.Player);

    private static IIncludableQueryable<GameTeamPlayer, TeamPlayer?> IncludeGameTeamGameTeamPlayersTeamPlayerRoot(this IQueryable<GameTeamPlayer> gameTeamPlayers)
        => gameTeamPlayers.IncludeGameTeamGameTeamPlayersRoot().ThenInclude(x => x.TeamPlayer);

    private static IIncludableQueryable<GameTeamPlayer, PlayerEntity?> IncludeGameTeamGameTeamPlayersTeamPlayerPlayerRoot(this IQueryable<GameTeamPlayer> gameTeamPlayers)
        => gameTeamPlayers.IncludeGameTeamGameTeamPlayersTeamPlayerRoot().ThenInclude(x => x!.Player);

    #endregion Private
}