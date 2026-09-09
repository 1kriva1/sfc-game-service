using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;

using SFC.Game.Application.Features.Game.Team.General.Commands.Create;
using SFC.Game.Domain.Enums.Invite;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Invite.Messages.Events.Invite.Game.Team;

namespace SFC.Game.Infrastructure.Consumers.Invite.Domain.Game.Team.Events;
public class GameTeamInviteAcceptedConsumer(IMapper mapper, ISender mediator) : IConsumer<GameTeamInviteUpdated>
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _mediator = mediator;

    public async Task Consume(ConsumeContext<GameTeamInviteUpdated> context)
    {
        GameTeamInviteUpdated @event = context.Message;

        CreateGameTeamCommand command = _mapper.Map<CreateGameTeamCommand>(@event);

        await _mediator.Send(command)
                       .ConfigureAwait(false);
    }
}

public class GameTeamInviteAcceptedDefinition : ConsumerDefinition<GameTeamInviteAcceptedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Invite.Value.Domain.Game.Team.Events.Updated; } }

    public GameTeamInviteAcceptedDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.invite.game.team.accepted.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<GameTeamInviteAcceptedConsumer> consumerConfigurator,
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
                x.RoutingKey = Enum.GetName<SFC.Game.Domain.Enums.Invite.InviteStatus>((InviteStatus)SFC.Game.Domain.Enums.Invite.InviteStatus.Accepted);
                x.ExchangeType = Exchange.Type;
            }));
        }
    }
}