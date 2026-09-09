using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;

using SFC.Game.Application.Features.Game.Player.Commands.Create;
using SFC.Game.Domain.Enums.Invite;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Invite.Messages.Events.Invite.Game.Player;
using SFC.Invite.Messages.Events.Invite.Game.Team;

namespace SFC.Game.Infrastructure.Consumers.Invite.Domain.Game.Player.Events;
public class GamePlayerInviteAcceptedConsumer(IMapper mapper, ISender mediator) : IConsumer<GamePlayerInviteUpdated>
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _mediator = mediator;

    public async Task Consume(ConsumeContext<GamePlayerInviteUpdated> context)
    {
        GamePlayerInviteUpdated @event = context.Message;

        CreateGamePlayerCommand command = _mapper.Map<CreateGamePlayerCommand>(@event);

        await _mediator.Send(command)
                       .ConfigureAwait(false);
    }
}

public class GamePlayerInviteAcceptedDefinition : ConsumerDefinition<GamePlayerInviteAcceptedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Invite.Value.Domain.Game.Player.Events.Updated; } }

    public GamePlayerInviteAcceptedDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.invite.game.player.accepted.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<GamePlayerInviteAcceptedConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.identity.Game.player.updated"
            rmq.Bind(Exchange.Name, (Action<IRabbitMqExchangeToExchangeBindingConfigurator>)(x =>
            {
                x.AutoDelete = true;
                x.RoutingKey = Enum.GetName<SFC.Game.Domain.Enums.Invite.InviteStatus>((InviteStatus)SFC.Game.Domain.Enums.Invite.InviteStatus.Accepted);
                x.ExchangeType = Exchange.Type;
            }));
        }
    }
}