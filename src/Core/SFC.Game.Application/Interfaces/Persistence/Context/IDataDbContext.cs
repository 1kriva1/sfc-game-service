using SFC.Game.Domain.Entities.Data;

namespace SFC.Game.Application.Interfaces.Persistence.Context;

/// <summary>
/// Data entities list.
/// Data service related entities.
/// </summary>
public interface IDataDbContext : IDbContext
{
    IQueryable<GameStyle> GameStyles { get; }

    IQueryable<FootballPosition> FootballPositions { get; }

    IQueryable<StatCategory> StatCategories { get; }

    IQueryable<StatSkill> StatSkills { get; }

    IQueryable<StatType> StatTypes { get; }

    IQueryable<WorkingFoot> WorkingFoots { get; }

    IQueryable<Shirt> Shirts { get; }
}