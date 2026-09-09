using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace SFC.Game.Infrastructure.Persistence.Extensions.Include.Player;

[Flags]
public enum PlayerIncludeOptions
{
    None = 0,
    Profile = 1 << 0,
}

public static class PlayerIncludeExtensions
{
    private static readonly Dictionary<string, PlayerIncludeOptions> IncludeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Profile"] = PlayerIncludeOptions.Profile
    };

    public static IQueryable<PlayerEntity> IncludePlayer(this IQueryable<PlayerEntity> query, PlayerIncludeOptions options)
    {
        if (options.HasFlag(PlayerIncludeOptions.Profile))
            query = query.IncludePlayerProfile();

        return query;
    }

    public static IQueryable<PlayerEntity> IncludePlayer(this IQueryable<PlayerEntity> query, PlayerIncludeOptions[] options)
    {
        foreach (PlayerIncludeOptions item in options)
        {
            query.IncludePlayer(item);
        }

        return query;
    }

    public static IQueryable<PlayerEntity> IncludePlayer(this IQueryable<PlayerEntity> query, IEnumerable<string>? includes, PlayerIncludeOptions defaultWhenEmpty = PlayerIncludeOptions.None)
    {
        PlayerIncludeOptions options = IncludeOptionsParser.Parse(includes, IncludeMap, defaultWhenEmpty);
        return query.IncludePlayer(options);
    }

    public static IQueryable<TSource> IncludePlayerDetails<TSource>(
    this IQueryable<TSource> source,
    Func<IQueryable<TSource>, IIncludableQueryable<TSource, PlayerEntity>> includePlayer)
    where TSource : class
    {
        return includePlayer(source)
            .ThenInclude(p => p.GeneralProfile)
            .IncludeFromPlayer(includePlayer, p => p.FootballProfile)
            .IncludeFromPlayer(includePlayer, p => p.Availability)
            .IncludeFromPlayer(includePlayer, p => p.Availability.Days)
            .IncludeFromPlayer(includePlayer, p => p.Points)
            .IncludeFromPlayer(includePlayer, p => p.Tags)
            .IncludeFromPlayer(includePlayer, p => p.Stats)
            .IncludeFromPlayer(includePlayer, p => p.Photo);
    }

    public static IQueryable<TSource> IncludeNestedPlayerDetails<TSource>(
    this IQueryable<TSource> source,
    Expression<Func<TSource, PlayerEntity>> playerNavigation)
    where TSource : class
    {
        return source.Include(playerNavigation).ThenInclude(p => p.GeneralProfile)
            .IncludeFromNestedPlayer(playerNavigation, p => p.FootballProfile)
            .IncludeFromNestedPlayer(playerNavigation, p => p.Availability)
            .IncludeFromNestedPlayer(playerNavigation, p => p.Availability.Days)
            .IncludeFromNestedPlayer(playerNavigation, p => p.Points)
            .IncludeFromNestedPlayer(playerNavigation, p => p.Tags)
            .IncludeFromNestedPlayer(playerNavigation, p => p.Stats)
            .IncludeFromNestedPlayer(playerNavigation, p => p.Photo);
    }

    #region Private

    private static IQueryable<PlayerEntity> IncludePlayerProfile(this IQueryable<PlayerEntity> players)
    {
        return players
            .Include(p => p.GeneralProfile)
            .Include(p => p.FootballProfile)
            .Include(p => p.Availability)
            .Include(p => p.Availability.Days)
            .Include(p => p.Points)
            .Include(p => p.Tags)
            .Include(p => p.Stats)
            .Include(p => p.Photo);
    }

    private static IQueryable<TSource> IncludeFromPlayer<TSource, TProperty>(
    this IQueryable<TSource> source,
    Func<IQueryable<TSource>, IIncludableQueryable<TSource, PlayerEntity>> includePlayer,
    Expression<Func<PlayerEntity, TProperty>> navigation)
    where TSource : class
    {
        return includePlayer(source).ThenInclude(navigation);
    }

    private static IQueryable<TSource> IncludeFromNestedPlayer<TSource, TProperty>(
        this IQueryable<TSource> source,
        Expression<Func<TSource, PlayerEntity>> playerNavigation,
        Expression<Func<PlayerEntity, TProperty>> navigation)
        where TSource : class
    {
        return source.Include(playerNavigation).ThenInclude(navigation);
    }

    #endregion
}