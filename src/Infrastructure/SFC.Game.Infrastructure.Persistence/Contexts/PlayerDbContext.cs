using Microsoft.EntityFrameworkCore;

using SFC.Game.Application.Interfaces.Persistence.Context;
using SFC.Game.Domain.Entities.Player;
using SFC.Game.Infrastructure.Persistence.Configurations.Player;
using SFC.Game.Infrastructure.Persistence.Constants;
using SFC.Game.Infrastructure.Persistence.Interceptors;

namespace SFC.Game.Infrastructure.Persistence.Contexts;
public class PlayerDbContext(
    DbContextOptions<PlayerDbContext> options,
    AuditableEntitySaveChangesInterceptor auditableInterceptor,
    UserEntitySaveChangesInterceptor userEntityInterceptor,
    DispatchDomainEventsSaveChangesInterceptor eventsInterceptor)
    : BaseDbContext<PlayerDbContext>(options, eventsInterceptor), IPlayerDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableInterceptor = auditableInterceptor;
    private readonly UserEntitySaveChangesInterceptor _userEntityInterceptor = userEntityInterceptor;

    public IQueryable<PlayerEntity> Players => Set<PlayerEntity>();

    public IQueryable<PlayerGeneralProfile> GeneralProfiles => Set<PlayerGeneralProfile>();

    public IQueryable<PlayerFootballProfile> FootballProfiles => Set<PlayerFootballProfile>();

    public IQueryable<PlayerAvailability> Availability => Set<PlayerAvailability>();

    public IQueryable<PlayerAvailableDay> AvailableDays => Set<PlayerAvailableDay>();

    public IQueryable<PlayerStatPoints> Points => Set<PlayerStatPoints>();

    public IQueryable<PlayerStat> Stats => Set<PlayerStat>();

    public IQueryable<PlayerTag> Tags => Set<PlayerTag>();

    public IQueryable<PlayerPhoto> Photos => Set<PlayerPhoto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseConstants.PlayerSchemaName);

        // data
        DataDbContext.ApplyDataConfigurations(modelBuilder);

        // identity
        IdentityDbContext.ApplyIdentityConfigurations(modelBuilder, Database.IsSqlServer());

        // player
        ApplyPlayerConfigurations(modelBuilder);

        // team
        TeamDbContext.ApplyTeamConfigurations(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableInterceptor);
        optionsBuilder.AddInterceptors(_userEntityInterceptor);
        base.OnConfiguring(optionsBuilder);
    }

    public static void ApplyPlayerConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PlayerAvailabilityConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerAvailableDayConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerFootballProfileConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerGeneralProfileConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerPhotoConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerPointsConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerStatConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerTagConfiguration());
    }
}