using System.Reflection;

using Azure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

using SFC.Game.Application.Interfaces.Common;
using SFC.Game.Application.Interfaces.Persistence.Context;
using SFC.Game.Domain.Entities.Game.Data;
using SFC.Game.Domain.Entities.Game.General;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Infrastructure.Persistence.Constants;

using SFC.Game.Infrastructure.Persistence.Interceptors;
using SFC.Game.Infrastructure.Persistence.Seeds;

namespace SFC.Game.Infrastructure.Persistence.Contexts;
public class GameDbContext(
    IDateTimeService dateTimeService,
    IHostEnvironment hostEnvironment,
    DbContextOptions<GameDbContext> options,
    AuditableEntitySaveChangesInterceptor auditableInterceptor,
    UserEntitySaveChangesInterceptor userEntityInterceptor,
    PlayerEntitySaveChangesInterceptor playerEntityInterceptor,
    TeamEntitySaveChangesInterceptor teamEntityInterceptor,
    DispatchDomainEventsSaveChangesInterceptor eventsInterceptor)
    : BaseDbContext<GameDbContext>(options, eventsInterceptor), IGameDbContext
{
#pragma warning disable CA1823 // Avoid unused private fields
    private readonly IDateTimeService _dateTimeService = dateTimeService;
#pragma warning restore CA1823 // Avoid unused private fields
    private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
    private readonly AuditableEntitySaveChangesInterceptor _auditableInterceptor = auditableInterceptor;
    private readonly UserEntitySaveChangesInterceptor _userEntityInterceptor = userEntityInterceptor;
    private readonly PlayerEntitySaveChangesInterceptor _playerEntityInterceptor = playerEntityInterceptor;
    private readonly TeamEntitySaveChangesInterceptor _teamEntityInterceptor = teamEntityInterceptor;

    #region General

    public IQueryable<GameEntity> Games => Set<GameEntity>();

    public IQueryable<GameGeneralProfile> GeneralProfiles => Set<GameGeneralProfile>();

    public IQueryable<GameFinancialProfile> FinancialProfiles => Set<GameFinancialProfile>();

    public IQueryable<GameInventaryProfile> InventaryProfiles => Set<GameInventaryProfile>();

    public IQueryable<GameAvailability> Availabilities => Set<GameAvailability>();

    public IQueryable<GameTag> Tags => Set<GameTag>();

    public IQueryable<GameTeam> GameTeams => Set<GameTeam>();

    public IQueryable<GamePlayer> GamePlayers => Set<GamePlayer>();

    public IQueryable<GameTeamPlayer> GameTeamPlayers => Set<GameTeamPlayer>();

    #endregion General

    #region Data

    public IQueryable<GameStatus> GameStatuses => Set<GameStatus>();

    public IQueryable<GameTeamStatus> GameTeamStatuses => Set<GameTeamStatus>();

    public IQueryable<GamePlayerStatus> GamePlayerStatuses => Set<GamePlayerStatus>();

    public IQueryable<GameTeamIndex> GameTeamIndexes => Set<GameTeamIndex>();

    #endregion Data

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseConstants.DefaultSchemaName);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // seed game data
        modelBuilder.SeedGameData(_dateTimeService);

        // invite
        InviteDbContext.ApplyInviteConfigurations(modelBuilder);

        // request
        RequestDbContext.ApplyRequestConfigurations(modelBuilder);

        // metadata
        MetadataDbContext.ApplyMetadataConfigurations(modelBuilder, _hostEnvironment.IsDevelopment());

        // identity
        IdentityDbContext.ApplyIdentityConfigurations(modelBuilder, Database.IsSqlServer());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableInterceptor);
        optionsBuilder.AddInterceptors(_userEntityInterceptor);
        optionsBuilder.AddInterceptors(_playerEntityInterceptor);
        optionsBuilder.AddInterceptors(_teamEntityInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}