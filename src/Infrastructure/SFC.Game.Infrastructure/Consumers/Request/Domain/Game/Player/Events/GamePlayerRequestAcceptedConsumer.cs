using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;

using SFC.Game.Application.Features.Game.Player.Commands.Create;
using SFC.Game.Domain.Enums.Request;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Request.Messages.Events.Request.Game.Player;
using SFC.Request.Messages.Events.Request.Game.Team;

namespace SFC.Game.Infrastructure.Consumers.Request.Domain.Game.Player.Events;
public class GamePlayerRequestAcceptedConsumer(IMapper mapper, ISender mediator) : IConsumer<GamePlayerRequestUpdated>
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _mediator = mediator;

    public async Task Consume(ConsumeContext<GamePlayerRequestUpdated> context)
    {
        GamePlayerRequestUpdated @event = context.Message;

        CreateGamePlayerCommand command = _mapper.Map<CreateGamePlayerCommand>(@event);

        await _mediator.Send(command)
                       .ConfigureAwait(false);
    }
}

public class GamePlayerRequestAcceptedDefinition : ConsumerDefinition<GamePlayerRequestAcceptedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Request.Value.Domain.Game.Player.Events.Updated; } }

    public GamePlayerRequestAcceptedDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.request.game.player.accepted.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<GamePlayerRequestAcceptedConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            rmq.Bind(Exchange.Name, (Action<IRabbitMqExchangeToExchangeBindingConfigurator>)(x =>
            {
                x.AutoDelete = true;
                x.RoutingKey = Enum.GetName<SFC.Game.Domain.Enums.Request.RequestStatus>((RequestStatus)SFC.Game.Domain.Enums.Request.RequestStatus.Accepted);
                x.ExchangeType = Exchange.Type;
            }));
        }
    }
}