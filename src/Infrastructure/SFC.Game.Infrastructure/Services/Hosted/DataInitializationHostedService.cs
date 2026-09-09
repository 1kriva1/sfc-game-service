using MassTransit;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Interfaces.Game.Data;
using SFC.Game.Application.Interfaces.Metadata;

namespace SFC.Game.Infrastructure.Services.Hosted;
public class DataInitializationHostedService(
    ILogger<DataInitializationHostedService> logger,
    IServiceProvider services) : BaseInitializationService(logger)
{
    private readonly IServiceProvider _services = services;

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        EventId eventId = new((int)RequestId.InitData, Enum.GetName(RequestId.InitData));
        Action<ILogger, Exception?> logStartExecution = LoggerMessage.Define(LogLevel.Information, eventId,
            "Data Initialization Hosted Service running.");
        logStartExecution(Logger, null);

        using IServiceScope scope = _services.CreateScope();

        // publish game data
        await PublishDataInitializedAsync(scope, cancellationToken).ConfigureAwait(false);

        // send require data
        await SendRequireDataAsync(scope, cancellationToken).ConfigureAwait(false);
    }

    private static async Task PublishDataInitializedAsync(IServiceScope scope, CancellationToken cancellationToken)
    {
        IGameDataService gameDataService = scope.ServiceProvider.GetRequiredService<IGameDataService>();

        await gameDataService.PublishDataInitializedEventAsync(cancellationToken).ConfigureAwait(false);

        IMetadataService metadataService = scope.ServiceProvider.GetRequiredService<IMetadataService>();

        await metadataService.CompleteAsync(MetadataServiceEnum.Game, MetadataDomainEnum.Data, MetadataTypeEnum.Initialization).ConfigureAwait(false);
    }

    private static Task SendRequireDataAsync(IServiceScope scope, CancellationToken cancellationToken)
    {
        // use bus because it is Initiator (reference to mass transit documentation)
        IBus bus = scope.ServiceProvider.GetRequiredService<IBus>();

        bus.Send(new SFC.Game.Messages.Commands.Data.RequireData(), cancellationToken);

        bus.Send(new SFC.Game.Messages.Commands.Team.Data.RequireData(), cancellationToken);

        bus.Send(new SFC.Game.Messages.Commands.Invite.Data.RequireData(), cancellationToken);

        bus.Send(new SFC.Game.Messages.Commands.Request.Data.RequireData(), cancellationToken);

        return Task.CompletedTask;
    }
}