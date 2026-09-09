using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SFC.Data.Messages.Events.Data;
using SFC.Game.Application.Features.Data.Commands.Reset;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;

namespace SFC.Game.Infrastructure.Consumers.Data;
public class DataInitializedConsumer(
    IMapper mapper,
    ILogger<DataInitializedConsumer> logger,
    ISender mediator) : IConsumer<DataInitialized>
{
    private readonly IMapper _mapper = mapper;
#pragma warning disable CA1823 // Avoid unused private fields
    private readonly ILogger<DataInitializedConsumer> _logger = logger;
#pragma warning restore CA1823 // Avoid unused private fields
    private readonly ISender _mediator = mediator;

    public async Task Consume(ConsumeContext<DataInitialized> context)
    {
        DataInitialized message = context.Message;

        ResetDataCommand command = _mapper.Map<ResetDataCommand>(message);

        await _mediator.Send(command)
                       .ConfigureAwait(false);
    }
}

public class DataInitializedDefinition : ConsumerDefinition<DataInitializedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Data.Value.Data.Source.Initialized; } }

    public DataInitializedDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.data.initialized.queue";
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

            // "sfc.data.init"
            rmq.Bind(Exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.ExchangeType = Exchange.Type;
            });
        }
    }
}