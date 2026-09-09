using AutoMapper;

using MassTransit;

using Microsoft.Extensions.Configuration;

using SFC.Game.Application.Interfaces.Game.Team.Player;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Game.Messages.Commands.Game.Team.Player;

namespace SFC.Game.Infrastructure.Consumers.Game.Domain.Team.Player.Seed;
public class RequireGameTeamPlayersSeedConsumer(IMapper mapper, IGameTeamPlayerSeedService gameTeamPlayerSeedService) : IConsumer<RequireGameTeamPlayersSeed>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamPlayerSeedService _gameTeamPlayerSeedService = gameTeamPlayerSeedService;

    public async Task Consume(ConsumeContext<RequireGameTeamPlayersSeed> context)
    {
        RequireGameTeamPlayersSeed message = context.Message;

        IEnumerable<GameTeamPlayer> gameTeamPlayers = await _gameTeamPlayerSeedService.GetSeedGameTeamPlayersAsync().ConfigureAwait(true);

        SeedGameTeamPlayers command = _mapper.Map<SeedGameTeamPlayers>(gameTeamPlayers)
                                             .SetCommandInitiator(message.Initiator);

        await context.Publish(command).ConfigureAwait(false);
    }
}

public class RequireGameTeamPlayersSeedDefinition : ConsumerDefinition<RequireGameTeamPlayersSeedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Message Exchange { get { return _settings.Exchanges.Game.Value.Domain.Team.Player.Seed.RequireSeed; } }

    public RequireGameTeamPlayersSeedDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.team.players.seed.require.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<RequireGameTeamPlayersSeedConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.game.team.players.seed.require"
            rmq.Bind(Exchange.Name, x => x.AutoDelete = true);
        }
    }
}