using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;

using SFC.Game.Application.Features.Game.Team.General.Commands.Create;
using SFC.Game.Domain.Enums.Request;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Request.Messages.Events.Request.Game.Team;

namespace SFC.Game.Infrastructure.Consumers.Request.Domain.Game.Team.Events;
public class GameTeamRequestAcceptedConsumer(IMapper mapper, ISender mediator) : IConsumer<GameTeamRequestUpdated>
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _mediator = mediator;

    public async Task Consume(ConsumeContext<GameTeamRequestUpdated> context)
    {
        GameTeamRequestUpdated @event = context.Message;

        CreateGameTeamCommand command = _mapper.Map<CreateGameTeamCommand>(@event);

        await _mediator.Send(command)
                       .ConfigureAwait(false);
    }
}

public class GameTeamRequestAcceptedDefinition : ConsumerDefinition<GameTeamRequestAcceptedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Request.Value.Domain.Game.Team.Events.Updated; } }

    public GameTeamRequestAcceptedDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.request.game.team.accepted.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<GameTeamRequestAcceptedConsumer> consumerConfigurator,
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