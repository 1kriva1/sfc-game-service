using Microsoft.EntityFrameworkCore;

using SFC.Game.Application.Interfaces.Persistence.Context;
using SFC.Game.Domain.Entities.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Data;
using SFC.Game.Infrastructure.Persistence.Constants;
using SFC.Game.Infrastructure.Persistence.Interceptors;

namespace SFC.Game.Infrastructure.Persistence.Contexts;
public class DataDbContext(
    DbContextOptions<DataDbContext> options,
    DataEntitySaveChangesInterceptor dataEntityInterceptor,
    DispatchDomainEventsSaveChangesInterceptor eventsInterceptor)
    : BaseDbContext<DataDbContext>(options, eventsInterceptor), IDataDbContext
{
    private readonly DataEntitySaveChangesInterceptor _dataEntityInterceptor = dataEntityInterceptor;

    public IQueryable<GameStyle> GameStyles => Set<GameStyle>();

    public IQueryable<FootballPosition> FootballPositions => Set<FootballPosition>();

    public IQueryable<StatCategory> StatCategories => Set<StatCategory>();

    public IQueryable<StatSkill> StatSkills => Set<StatSkill>();

    public IQueryable<StatType> StatTypes => Set<StatType>();

    public IQueryable<WorkingFoot> WorkingFoots => Set<WorkingFoot>();

    public IQueryable<Shirt> Shirts => Set<Shirt>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseConstants.DataSchemaName);

        // data
        ApplyDataConfigurations(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Example:
    /// modelBuilder.ApplyConfiguration(new ShirtConfiguration());
    /// </summary>
    /// <param name="modelBuilder">Model builder</param>
    public static void ApplyDataConfigurations(ModelBuilder modelBuilder)
    {
        // add data configurations
        modelBuilder.ApplyConfiguration(new GameStyleConfiguration());

        modelBuilder.ApplyConfiguration(new FootballPositionConfiguration());

        modelBuilder.ApplyConfiguration(new StatCategoryConfiguration());

        modelBuilder.ApplyConfiguration(new StatSkillConfiguration());

        modelBuilder.ApplyConfiguration(new StatTypeConfiguration());

        modelBuilder.ApplyConfiguration(new WorkingFootConfiguration());

        modelBuilder.ApplyConfiguration(new ShirtConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_dataEntityInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}