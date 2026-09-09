using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Entities.Team.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Team;

namespace SFC.Game.Infrastructure.Persistence.Extensions.Include.Game;

[Flags]
public enum GameTeamIncludeOptions
{
    None = 0,
    Team = 1 << 1,
    TeamProfile = 1 << 2,
    TeamWithTeamPlayers = 1 << 3,
    TeamWithTeamPlayersWithPlayer = 1 << 4,
    TeamWithTeamPlayersWithPlayerProfile = 1 << 5,
    GameTeamPlayers = 1 << 6,
    GameTeamPlayersWithPlayer = 1 << 7,
    GameTeamPlayersWithPlayerProfile = 1 << 8
}

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
        ["GameTeamPlayers.Player"] = GameTeamIncludeOptions.GameTeamPlayersWithPlayer,
        ["GameTeamPlayers.PlayerProfile"] = GameTeamIncludeOptions.GameTeamPlayersWithPlayerProfile
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

        if (options.HasFlag(GameTeamIncludeOptions.GameTeamPlayersWithPlayer))
            query = query.IncludeGameTeamPlayersRoot().ThenInclude(x => x.Player);

        if (options.HasFlag(GameTeamIncludeOptions.GameTeamPlayersWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeGameTeamPlayersRoot().ThenInclude(x => x.Player));

        return query;
    }

    public static IQueryable<GameTeam> IncludeGameTeam(this IQueryable<GameTeam> query, params GameTeamIncludeOptions[] options)
    {
        foreach (GameTeamIncludeOptions option in options)
        {
            query.IncludeGameTeam(option);
        }

        return query;
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

    #endregion Private
}