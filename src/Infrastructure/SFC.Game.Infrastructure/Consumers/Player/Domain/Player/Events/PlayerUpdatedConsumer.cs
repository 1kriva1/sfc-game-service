using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SFC.Game.Application.Features.Player.Commands.Create;
using SFC.Game.Application.Features.Player.Commands.Update;
using SFC.Game.Application.Interfaces.Persistence.Repository.Player;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Player.Messages.Events.Player.General;

namespace SFC.Game.Infrastructure.Consumers.Player.Domain.Player.Events;
public class PlayerUpdatedConsumer(
    IMapper mapper,
    ILogger<PlayerUpdatedConsumer> logger,
    ISender mediator,
    IPlayerRepository playerRepository) : IConsumer<PlayerUpdated>
{
#pragma warning disable CA1823 // Avoid unused private fields
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<PlayerUpdatedConsumer> _logger = logger;
    private readonly ISender _mediator = mediator;
    private readonly IPlayerRepository _playerRepository = playerRepository;
#pragma warning restore CA1823 // Avoid unused private fields

    public async Task Consume(ConsumeContext<PlayerUpdated> context)
    {
        PlayerUpdated @event = context.Message;

        bool playerExist = await _playerRepository.AnyAsync(@event.Player.Id)
                                                  .ConfigureAwait(true);

        if (playerExist)
            await UpdatePlayerAsync(@event).ConfigureAwait(false);
        else
            await CreatePlayerAsync(@event).ConfigureAwait(false);
    }

    private Task UpdatePlayerAsync(PlayerUpdated @event)
    {
        UpdatePlayerCommand command = _mapper.Map<UpdatePlayerCommand>(@event);
        return _mediator.Send(command);
    }

    private Task CreatePlayerAsync(PlayerUpdated @event)
    {
        CreatePlayerCommand command = _mapper.Map<CreatePlayerCommand>(@event);
        return _mediator.Send(command);
    }
}

public class PlayerUpdatedConsumerDefinition : ConsumerDefinition<PlayerUpdatedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Player.Value.Domain.Player.Events.Updated; } }

    public PlayerUpdatedConsumerDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.player.updated.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<PlayerUpdatedConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.player.updated"
            rmq.Bind(Exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.ExchangeType = Exchange.Type;
            });
        }
    }
}