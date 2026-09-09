using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SFC.Game.Application.Features.Team.Player.Commands.CreateRange;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Team.Messages.Commands.Team.Player;

namespace SFC.Game.Infrastructure.Consumers.Team.Domain.Player.Seed;
public class SeedTeamPlayersConsumer(
    IMapper mapper,
    IWebHostEnvironment environment,
    ILogger<SeedTeamPlayersConsumer> logger,
    ISender mediator) : IConsumer<SeedTeamPlayers>
{
#pragma warning disable CA1823 // Avoid unused private fields
    private readonly IMapper _mapper = mapper;
    private readonly IWebHostEnvironment _environment = environment;
    private readonly ILogger<SeedTeamPlayersConsumer> _logger = logger;
    private readonly ISender _mediator = mediator;
#pragma warning restore CA1823 // Avoid unused private fields

    public async Task Consume(ConsumeContext<SeedTeamPlayers> context)
    {
        if (_environment.IsDevelopment())
        {
            SeedTeamPlayers message = context.Message;

            CreateTeamPlayersCommand command = _mapper.Map<CreateTeamPlayersCommand>(message.TeamPlayers);

            await _mediator.Send(command)
                           .ConfigureAwait(false);
        }
    }
}

public class SeedTeamPlayersConsumerDefinition : ConsumerDefinition<SeedTeamPlayersConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Team.Value.Domain.Player.Seed.Seed; } }

    public SeedTeamPlayersConsumerDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.team.players.seed.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<SeedTeamPlayersConsumer> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            rmq.Bind(Exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.RoutingKey = _settings.Exchanges.Game.Key.BuildExchangeRoutingKey(_settings.Exchanges.Team.Key);
                x.ExchangeType = Exchange.Type;
            });
        }
    }
}