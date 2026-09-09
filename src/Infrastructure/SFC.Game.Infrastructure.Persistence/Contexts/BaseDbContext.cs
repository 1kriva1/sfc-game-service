using Microsoft.EntityFrameworkCore;

using SFC.Game.Infrastructure.Persistence.Interceptors;

namespace SFC.Game.Infrastructure.Persistence.Contexts;
public abstract class BaseDbContext<TContext>(
    DbContextOptions<TContext> options,
    DispatchDomainEventsSaveChangesInterceptor eventsInterceptor) : DbContext(options)
    where TContext : DbContext
{
    private readonly DispatchDomainEventsSaveChangesInterceptor _eventsInterceptor = eventsInterceptor;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ArgumentNullException.ThrowIfNull(optionsBuilder);
        optionsBuilder.AddInterceptors(_eventsInterceptor);
    }
}