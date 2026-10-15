using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Entities.Team.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Team;

namespace SFC.Game.Infrastructure.Persistence.Extensions.Include.Game;

public static class GameTeamIncludeExtensions
{
    private static readonly Dictionary<string, GameTeamIncludeOptions> IncludeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Team"] = GameTeamIncludeOptions.Team,
        ["TeamProfile"] = GameTeamIncludeOptions.TeamProfile,
        ["Team.TeamPlayers"] = GameTeamIncludeOptions.TeamWithTeamPlayers,
        ["Team.TeamPlayers.Player"] = GameTeamIncludeOptions.TeamWithTeamPlayersWithPlayer,
        ["Team.TeamPlayers.PlayerProfile"] = GameTeamIncludeOptions.TeamWithTeamPlayersWithPlayerProfile,
        ["GameTeamPlayers"] = GameTeamIncludeOptions.GameTeamPlayers,
        ["GameTeamPlayers.GamePlayer"] = GameTeamIncludeOptions.GameTeamPlayersWithGamePlayer,
        ["GameTeamPlayers.GamePlayer.Player"] = GameTeamIncludeOptions.GameTeamPlayersWithGamePlayerWithPlayer,
        ["GameTeamPlayers.GamePlayer.PlayerProfile"] = GameTeamIncludeOptions.GameTeamPlayersWithGamePlayerWithPlayerProfile,
        ["GameTeamPlayers.TeamPlayer"] = GameTeamIncludeOptions.GameTeamPlayersWithTeamPlayer,
        ["GameTeamPlayers.TeamPlayer.Player"] = GameTeamIncludeOptions.GameTeamPlayersWithTeamPlayerWithPlayer,
        ["GameTeamPlayers.TeamPlayer.PlayerProfile"] = GameTeamIncludeOptions.GameTeamPlayersWithTeamPlayerWithPlayerProfile
    };

    public static IQueryable<GameTeam> IncludeGameTeam(this IQueryable<GameTeam> query, GameTeamIncludeOptions options)
    {
        // team
        if (options.HasFlag(GameTeamIncludeOptions.Team))
            query = query.IncludeTeamRoot();

        if (options.HasFlag(GameTeamIncludeOptions.TeamProfile))
            query = query.IncludeTeamDetails(q => q.Include(x => x.Team));

        if (options.HasFlag(GameTeamIncludeOptions.TeamWithTeamPlayers))
            query = query.IncludeTeamTeamPlayersRoot();

        if (options.HasFlag(GameTeamIncludeOptions.TeamWithTeamPlayersWithPlayer))
            query = query.IncludeTeamTeamPlayersRoot().ThenInclude(x => x.Player);

        if (options.HasFlag(GameTeamIncludeOptions.TeamWithTeamPlayersWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeTeamTeamPlayersRoot().ThenInclude(x => x.Player));

        if (options.HasFlag(GameTeamIncludeOptions.GameTeamPlayers))
            query = query.IncludeGameTeamPlayersRoot();

        if (options.HasFlag(GameTeamIncludeOptions.GameTeamPlayersWithGamePlayer))
            query = query.IncludeGameTeamPlayersGamePlayerRoot();

        if (options.HasFlag(GameTeamIncludeOptions.GameTeamPlayersWithGamePlayerWithPlayer))
            query = query.IncludeGameTeamPlayersGamePlayerPlayerRoot();

        if (options.HasFlag(GameTeamIncludeOptions.GameTeamPlayersWithGamePlayerWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGameTeamPlayersGamePlayerPlayerRoot()!);

        if (options.HasFlag(GameTeamIncludeOptions.GameTeamPlayersWithTeamPlayer))
            query = query.IncludeGameTeamPlayersTeamPlayerRoot();

        if (options.HasFlag(GameTeamIncludeOptions.GameTeamPlayersWithTeamPlayerWithPlayer))
            query = query.IncludeGameTeamPlayersTeamPlayerPlayerRoot();

        if (options.HasFlag(GameTeamIncludeOptions.GameTeamPlayersWithTeamPlayerWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGameTeamPlayersTeamPlayerPlayerRoot()!);

        return query;
    }

    public static IQueryable<GameTeam> IncludeGameTeam(this IQueryable<GameTeam> query, params GameTeamIncludeOptions[] options)
    {
        GameTeamIncludeOptions merged = GameTeamIncludeOptions.None;

        foreach (GameTeamIncludeOptions option in options)
        {
            merged |= option;
        }

        return query.IncludeGameTeam(merged);
    }

    public static IQueryable<GameTeam> IncludeGameTeam(this IQueryable<GameTeam> query, IEnumerable<string>? includes, GameTeamIncludeOptions defaultWhenEmpty = GameTeamIncludeOptions.None)
    {
        GameTeamIncludeOptions options = IncludeOptionsParser.Parse(includes, IncludeMap, defaultWhenEmpty);
        return query.IncludeGameTeam(options);
    }

    #region Private

    private static IIncludableQueryable<GameTeam, TeamEntity> IncludeTeamRoot(this IQueryable<GameTeam> gameTeams)
        => gameTeams.Include(x => x.Team);

    private static IIncludableQueryable<GameTeam, ICollection<TeamPlayer>> IncludeTeamTeamPlayersRoot(this IQueryable<GameTeam> gameTeams)
        => gameTeams.IncludeTeamRoot().ThenInclude(x => x.Players);

    private static IIncludableQueryable<GameTeam, ICollection<GameTeamPlayer>> IncludeGameTeamPlayersRoot(this IQueryable<GameTeam> gameTeams)
        => gameTeams.Include(x => x.Players);

    private static IIncludableQueryable<GameTeam, GamePlayer?> IncludeGameTeamPlayersGamePlayerRoot(this IQueryable<GameTeam> gameTeams)
        => gameTeams.IncludeGameTeamPlayersRoot().ThenInclude(x => x.GamePlayer);

    private static IIncludableQueryable<GameTeam, PlayerEntity?> IncludeGameTeamPlayersGamePlayerPlayerRoot(this IQueryable<GameTeam> gameTeams)
        => gameTeams.IncludeGameTeamPlayersGamePlayerRoot().ThenInclude(x => x!.Player);

    private static IIncludableQueryable<GameTeam, TeamPlayer?> IncludeGameTeamPlayersTeamPlayerRoot(this IQueryable<GameTeam> gameTeams)
        => gameTeams.IncludeGameTeamPlayersRoot().ThenInclude(x => x.TeamPlayer);

    private static IIncludableQueryable<GameTeam, PlayerEntity?> IncludeGameTeamPlayersTeamPlayerPlayerRoot(this IQueryable<GameTeam> gameTeams)
       => gameTeams.IncludeGameTeamPlayersTeamPlayerRoot().ThenInclude(x => x!.Player);

    #endregion Private
}