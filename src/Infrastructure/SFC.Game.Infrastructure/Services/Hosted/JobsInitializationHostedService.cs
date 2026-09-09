using Hangfire;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Common.Settings;
using SFC.Game.Application.Interfaces.Cache;

namespace SFC.Game.Infrastructure.Services.Hosted;
public class JobsInitializationHostedService(
    ILogger<JobsInitializationHostedService> logger,
    IServiceProvider services,
    IOptions<CacheSettings> cacheConfig) : BaseInitializationService(logger)
{
    private readonly ILogger<JobsInitializationHostedService> _logger = logger;
    private readonly IServiceProvider _services = services;
    private readonly IOptions<CacheSettings> _cacheConfig = cacheConfig;

    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        Action<ILogger, Exception?> logRequest = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(),
                "Jobs Initialization Hosted Service running."
        );
        logRequest(_logger, null);

        using IServiceScope scope = _services.CreateScope();

        if (_cacheConfig.Value.Enabled)
        {
            AddCacheRefreshJob(cancellationToken);
        }

        return Task.CompletedTask;
    }

    private void AddCacheRefreshJob(CancellationToken cancellationToken)
    {
        RecurringJob.AddOrUpdate<IRefreshCache>(Enum.GetName<Job>(Job.RefreshCache),
            s => s.RefreshAsync(cancellationToken), _cacheConfig.Value.Refresh.Cron);
    }
}