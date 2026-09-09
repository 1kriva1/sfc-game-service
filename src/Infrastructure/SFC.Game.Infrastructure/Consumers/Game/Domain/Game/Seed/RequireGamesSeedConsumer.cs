using AutoMapper;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SFC.Game.Application.Interfaces.Game.General;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Game.Messages.Commands.Game.General;

namespace SFC.Game.Infrastructure.Consumers.Game.Domain.Game.Seed;
public class RequireGamesSeedConsumer(
    ILogger<RequireGamesSeedConsumer> logger,
    IMapper mapper,
    IGameSeedService gameSeedService) : IConsumer<RequireGamesSeed>
{
#pragma warning disable CA1823 // Avoid unused private fields
    private readonly ILogger<RequireGamesSeedConsumer> _logger = logger;
#pragma warning restore CA1823 // Avoid unused private fields
    private readonly IMapper _mapper = mapper;
    private readonly IGameSeedService _gameSeedService = gameSeedService;

    public async Task Consume(ConsumeContext<RequireGamesSeed> context)
    {
        RequireGamesSeed message = context.Message;

        IEnumerable<GameEntity> games = await _gameSeedService.GetSeedGamesAsync().ConfigureAwait(true);

        SeedGames command = _mapper.Map<SeedGames>(games)
                                   .SetCommandInitiator(message.Initiator);

        await context.Publish(command).ConfigureAwait(false);
    }
}

public class RequireGamesSeedDefinition : ConsumerDefinition<RequireGamesSeedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Message Exchange { get { return _settings.Exchanges.Game.Value.Domain.Game.Seed.RequireSeed; } }

    public RequireGamesSeedDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.games.seed.require.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<RequireGamesSeedConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            rmq.Bind(Exchange.Name, x => x.AutoDelete = true);
        }
    }
}