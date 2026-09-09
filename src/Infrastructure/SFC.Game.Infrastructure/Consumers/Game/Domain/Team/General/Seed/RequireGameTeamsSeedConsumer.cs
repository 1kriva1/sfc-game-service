using AutoMapper;

using MassTransit;

using Microsoft.Extensions.Configuration;

using SFC.Game.Application.Interfaces.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Game.Messages.Commands.Game.Team.General;

namespace SFC.Game.Infrastructure.Consumers.Game.Domain.Team.General.Seed;
public class RequireGameTeamsSeedConsumer(IMapper mapper, IGameTeamSeedService gameTeamSeedService) : IConsumer<RequireGameTeamsSeed>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamSeedService _gameTeamSeedService = gameTeamSeedService;

    public async Task Consume(ConsumeContext<RequireGameTeamsSeed> context)
    {
        RequireGameTeamsSeed message = context.Message;

        IEnumerable<GameTeam> gameTeams = await _gameTeamSeedService.GetSeedGameTeamsAsync().ConfigureAwait(true);

        SeedGameTeams command = _mapper.Map<SeedGameTeams>(gameTeams)
                                         .SetCommandInitiator(message.Initiator);

        await context.Publish(command).ConfigureAwait(false);
    }
}

public class RequireGameTeamsSeedDefinition : ConsumerDefinition<RequireGameTeamsSeedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Message Exchange { get { return _settings.Exchanges.Game.Value.Domain.Team.Team.Seed.RequireSeed; } }

    public RequireGameTeamsSeedDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.team.teams.seed.require.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<RequireGameTeamsSeedConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.game.teams.seed.require"
            rmq.Bind(Exchange.Name, x => x.AutoDelete = true);
        }
    }
}