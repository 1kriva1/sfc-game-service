using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

using SFC.Game.Application.Interfaces.Common;
using SFC.Game.Domain.Common.Interfaces;

namespace SFC.Game.Infrastructure.Persistence.Interceptors;
public class DataEntitySaveChangesInterceptor(IDateTimeService dateTimeService) : SaveChangesInterceptor
{
    private readonly IDateTimeService _dateTimeService = dateTimeService;

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        IEnumerable<EntityEntry<IDataEntity>> entries = context.ChangeTracker.Entries<IDataEntity>();

        foreach (EntityEntry<IDataEntity> entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = _dateTimeService.Now;
            }
        }
    }
}