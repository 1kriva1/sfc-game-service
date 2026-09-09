using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;

using SFC.Game.Application.Features.Invite.Data.Commands.Reset;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Game.Messages.Commands.Invite.Data;

namespace SFC.Game.Infrastructure.Consumers.Game.Data.Invite;
public class InitializeDataConsumer(IMapper mapper, ISender mediator) : IConsumer<InitializeData>
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _mediator = mediator;

    public async Task Consume(ConsumeContext<InitializeData> context)
    {
        InitializeData message = context.Message;

        ResetInviteDataCommand command = _mapper.Map<ResetInviteDataCommand>(message);

        await _mediator.Send(command)
                       .ConfigureAwait(false);
    }
}

public class RequireDataConsumerDefinition : ConsumerDefinition<InitializeDataConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Game.Value.Data.Dependent.Invite.Initialize; } }

    public RequireDataConsumerDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.invite.data.initialize.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<InitializeDataConsumer> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            rmq.Bind(Exchange.Name, x => x.AutoDelete = true);
        }
    }
}