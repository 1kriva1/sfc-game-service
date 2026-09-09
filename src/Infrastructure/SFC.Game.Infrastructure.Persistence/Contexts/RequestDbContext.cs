using Microsoft.EntityFrameworkCore;

using SFC.Game.Application.Interfaces.Persistence.Context;
using SFC.Game.Domain.Entities.Request.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Request.Data;
using SFC.Game.Infrastructure.Persistence.Constants;
using SFC.Game.Infrastructure.Persistence.Interceptors;

namespace SFC.Game.Infrastructure.Persistence.Contexts;
public class RequestDbContext(
    DbContextOptions<RequestDbContext> options,
    DataEntitySaveChangesInterceptor dataEntityInterceptor,
    DispatchDomainEventsSaveChangesInterceptor eventsInterceptor)
    : BaseDbContext<RequestDbContext>(options, eventsInterceptor), IRequestDbContext
{
    private readonly DataEntitySaveChangesInterceptor _dataEntityInterceptor = dataEntityInterceptor;

    #region Data

    public IQueryable<RequestStatus> RequestStatuses => Set<RequestStatus>();

    #endregion Data

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseConstants.DataSchemaName);

        // data
        ApplyRequestConfigurations(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    public static void ApplyRequestConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RequestStatusConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_dataEntityInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}