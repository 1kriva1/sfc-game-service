using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;

using SFC.Game.Application.Features.Request.Data.Commands.Reset;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Request.Messages.Events.Request.Data;

namespace SFC.Game.Infrastructure.Consumers.Request.Data;
public class DataInitializedConsumer(IMapper mapper, ISender mediator) : IConsumer<DataInitialized>
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _mediator = mediator;

    public async Task Consume(ConsumeContext<DataInitialized> context)
    {
        DataInitialized message = context.Message;

        ResetRequestDataCommand command = _mapper.Map<ResetRequestDataCommand>(message);

        await _mediator.Send(command)
                       .ConfigureAwait(false);
    }
}

public class DataInitializedDefinition : ConsumerDefinition<DataInitializedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Request.Value.Data.Source.Initialized; } }

    public DataInitializedDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.request.data.initialized.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<DataInitializedConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            rmq.Bind(Exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.ExchangeType = Exchange.Type;
            });
        }
    }
}