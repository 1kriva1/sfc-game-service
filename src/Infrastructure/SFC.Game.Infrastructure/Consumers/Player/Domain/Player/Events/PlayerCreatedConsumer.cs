using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SFC.Game.Application.Features.Player.Commands.Create;
using SFC.Game.Application.Interfaces.Persistence.Repository.Player;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Player.Messages.Events.Player.General;

namespace SFC.Game.Infrastructure.Consumers.Player.Domain.Player.Events;
public class PlayerCreatedConsumer(
    IMapper mapper,
    ILogger<PlayerCreatedConsumer> logger,
    ISender mediator,
    IPlayerRepository playerRepository) : IConsumer<PlayerCreated>
{
#pragma warning disable CA1823 // Avoid unused private fields
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<PlayerCreatedConsumer> _logger = logger;
    private readonly ISender _mediator = mediator;
    private readonly IPlayerRepository _playerRepository = playerRepository;
#pragma warning restore CA1823 // Avoid unused private fields

    public async Task Consume(ConsumeContext<PlayerCreated> context)
    {
        PlayerCreated @event = context.Message;

        bool playerExist = await _playerRepository.AnyAsync(@event.Player.Id)
                                                  .ConfigureAwait(true);

        if (!playerExist)
        {
            CreatePlayerCommand command = _mapper.Map<CreatePlayerCommand>(@event);

            await _mediator.Send(command)
                           .ConfigureAwait(false);
        }
    }
}

public class PlayerCreatedConsumerDefinition : ConsumerDefinition<PlayerCreatedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Player.Value.Domain.Player.Events.Created; } }

    public PlayerCreatedConsumerDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.player.created.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<PlayerCreatedConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.player.created"
            rmq.Bind(Exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.ExchangeType = Exchange.Type;
            });
        }
    }
}