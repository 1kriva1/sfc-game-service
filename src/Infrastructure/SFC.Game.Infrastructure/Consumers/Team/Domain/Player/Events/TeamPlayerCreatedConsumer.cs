using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SFC.Game.Application.Features.Team.Player.Commands.Create;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Team.Messages.Events.Team.Player;

namespace SFC.Game.Infrastructure.Consumers.Team.Domain.Player.Events;
public class TeamPlayerCreatedConsumer(
    IMapper mapper,
    ILogger<TeamPlayerCreatedConsumer> logger,
    ISender mediator) : IConsumer<TeamPlayerCreated>
{
#pragma warning disable CA1823 // Avoid unused private fields
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<TeamPlayerCreatedConsumer> _logger = logger;
    private readonly ISender _mediator = mediator;
#pragma warning restore CA1823 // Avoid unused private fields

    public async Task Consume(ConsumeContext<TeamPlayerCreated> context)
    {
        TeamPlayerCreated @event = context.Message;

        CreateTeamPlayerCommand command = _mapper.Map<CreateTeamPlayerCommand>(@event);

        await _mediator.Send(command)
                       .ConfigureAwait(false);
    }
}

public class TeamPlayerCreatedConsumerDefinition : ConsumerDefinition<TeamPlayerCreatedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Team.Value.Domain.Player.Events.Created; } }

    public TeamPlayerCreatedConsumerDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.team.player.created.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<TeamPlayerCreatedConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.team.player.created"
            rmq.Bind(Exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.ExchangeType = Exchange.Type;
            });
        }
    }
}