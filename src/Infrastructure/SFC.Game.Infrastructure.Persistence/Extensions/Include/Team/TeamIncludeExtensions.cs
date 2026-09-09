using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using SFC.Game.Domain.Entities.Team.Player;
using SFC.Game.Infrastructure.Persistence.Extensions.Include.Player;

namespace SFC.Game.Infrastructure.Persistence.Extensions.Include.Team;

[Flags]
public enum TeamIncludeOptions
{
    None = 0,
    Profile = 1 << 0,
    TeamPlayers = 1 << 1,
    TeamPlayersWithPlayer = 1 << 2,
    TeamPlayersWithPlayerProfile = 1 << 3,
}

public static class TeamIncludeExtensions
{
    private static readonly Dictionary<string, TeamIncludeOptions> IncludeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Profile"] = TeamIncludeOptions.Profile,
        ["TeamPlayers"] = TeamIncludeOptions.TeamPlayers,
        ["TeamPlayers.Player"] = TeamIncludeOptions.TeamPlayersWithPlayer,
        ["TeamPlayers.PlayerProfile"] = TeamIncludeOptions.TeamPlayersWithPlayerProfile,
    };

    public static IQueryable<TeamEntity> IncludeTeam(this IQueryable<TeamEntity> query, TeamIncludeOptions options)
    {
        if (options.HasFlag(TeamIncludeOptions.Profile))
            query = query.IncludeTeamProfile();

        if (options.HasFlag(TeamIncludeOptions.TeamPlayers))
            query = query.IncludeTeamPlayersRoot();

        if (options.HasFlag(TeamIncludeOptions.TeamPlayersWithPlayer))
            query = query.IncludeTeamPlayersRoot().ThenInclude(x => x.Player);

        if (options.HasFlag(TeamIncludeOptions.TeamPlayersWithPlayerProfile))
            query = query.IncludePlayerDetails(q => q.IncludeTeamPlayersRoot().ThenInclude(x => x.Player));

        return query;
    }

    public static IQueryable<TeamEntity> IncludeTeam(this IQueryable<TeamEntity> query, TeamIncludeOptions[] options)
    {
        foreach (TeamIncludeOptions item in options)
        {
            query.IncludeTeam(item);
        }

        return query;
    }

    public static IQueryable<TeamEntity> IncludeTeam(this IQueryable<TeamEntity> query, IEnumerable<string>? includes, TeamIncludeOptions defaultWhenEmpty = TeamIncludeOptions.None)
    {
        TeamIncludeOptions options = IncludeOptionsParser.Parse(includes, IncludeMap, defaultWhenEmpty);
        return query.IncludeTeam(options);
    }

    public static IQueryable<TSource> IncludeTeamDetails<TSource>(
    this IQueryable<TSource> source,
    Func<IQueryable<TSource>, IIncludableQueryable<TSource, TeamEntity>> includeTeam)
    where TSource : class
    {
        return includeTeam(source)
            .ThenInclude(p => p.GeneralProfile)
            .IncludeFromTeam(includeTeam, p => p.FinancialProfile)
            .IncludeFromTeam(includeTeam, p => p.InventaryProfile)
            .IncludeFromTeam(includeTeam, p => p.Availability)
            .IncludeFromTeam(includeTeam, p => p.Shirts)
            .IncludeFromTeam(includeTeam, p => p.Tags)
            .IncludeFromTeam(includeTeam, p => p.Logo);
    }

    public static IQueryable<TSource> IncludeNestedTeamDetails<TSource>(
    this IQueryable<TSource> source,
    Expression<Func<TSource, TeamEntity>> teamNavigation)
    where TSource : class
    {
        return source.Include(teamNavigation)
            .ThenInclude(p => p.GeneralProfile)
            .IncludeFromNestedTeam(teamNavigation, p => p.FinancialProfile)
            .IncludeFromNestedTeam(teamNavigation, p => p.InventaryProfile)
            .IncludeFromNestedTeam(teamNavigation, p => p.Availability)
            .IncludeFromNestedTeam(teamNavigation, p => p.Shirts)
            .IncludeFromNestedTeam(teamNavigation, p => p.Tags)
            .IncludeFromNestedTeam(teamNavigation, p => p.Logo);
    }

    #region Private

    private static IIncludableQueryable<TeamEntity, ICollection<TeamPlayer>> IncludeTeamPlayersRoot(this IQueryable<TeamEntity> teams)
        => teams.Include(x => x.Players);

    private static IQueryable<TeamEntity> IncludeTeamProfile(this IQueryable<TeamEntity> teams)
    {
        return teams
            .Include(p => p.GeneralProfile)
            .Include(p => p.InventaryProfile)
            .Include(p => p.FinancialProfile)
            .Include(p => p.Shirts)
            .Include(p => p.Availability)
            .Include(p => p.Tags)
            .Include(p => p.Logo);
    }

    private static IQueryable<TSource> IncludeFromTeam<TSource, TProperty>(
        this IQueryable<TSource> source,
        Func<IQueryable<TSource>, IIncludableQueryable<TSource, TeamEntity>> includeTeam,
        Expression<Func<TeamEntity, TProperty>> navigation)
        where TSource : class
    {
        return includeTeam(source).ThenInclude(navigation);
    }

    private static IQueryable<TSource> IncludeFromNestedTeam<TSource, TProperty>(
        this IQueryable<TSource> source,
        Expression<Func<TSource, TeamEntity>> teamNavigation,
        Expression<Func<TeamEntity, TProperty>> navigation)
        where TSource : class
    {
        return source.Include(teamNavigation).ThenInclude(navigation);
    }

    #endregion
}