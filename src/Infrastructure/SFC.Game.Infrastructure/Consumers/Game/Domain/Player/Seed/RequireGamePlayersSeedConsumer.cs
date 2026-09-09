using AutoMapper;

using MassTransit;

using Microsoft.Extensions.Configuration;

using SFC.Game.Application.Interfaces.Game.Player;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Game.Messages.Commands.Game.Player;

namespace SFC.Game.Infrastructure.Consumers.Game.Domain.Player.Seed;
public class RequireGamePlayersSeedConsumer(IMapper mapper, IGamePlayerSeedService gamePlayerSeedService) : IConsumer<RequireGamePlayersSeed>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGamePlayerSeedService _gamePlayerSeedService = gamePlayerSeedService;

    public async Task Consume(ConsumeContext<RequireGamePlayersSeed> context)
    {
        RequireGamePlayersSeed message = context.Message;

        IEnumerable<GamePlayer> gamePlayers = await _gamePlayerSeedService.GetSeedGamePlayersAsync().ConfigureAwait(true);

        SeedGamePlayers command = _mapper.Map<SeedGamePlayers>(gamePlayers)
                                         .SetCommandInitiator(message.Initiator);

        await context.Publish(command).ConfigureAwait(false);
    }
}

public class RequireGamePlayersSeedDefinition : ConsumerDefinition<RequireGamePlayersSeedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Message Exchange { get { return _settings.Exchanges.Game.Value.Domain.Player.Seed.RequireSeed; } }

    public RequireGamePlayersSeedDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.players.seed.require.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<RequireGamePlayersSeedConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.game.players.seed.require"
            rmq.Bind(Exchange.Name, x => x.AutoDelete = true);
        }
    }
}