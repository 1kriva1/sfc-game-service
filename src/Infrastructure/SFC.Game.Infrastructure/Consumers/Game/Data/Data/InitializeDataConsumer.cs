using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SFC.Game.Application.Features.Data.Commands.Reset;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Game.Messages.Commands.Data;

namespace SFC.Game.Infrastructure.Consumers.Game.Data.Data;
public class InitializeDataConsumer(
    IMapper mapper,
    ILogger<InitializeDataConsumer> logger,
    ISender mediator) : IConsumer<InitializeData>
{
    private readonly IMapper _mapper = mapper;
#pragma warning disable CA1823 // Avoid unused private fields
    private readonly ILogger<InitializeDataConsumer> _logger = logger;
#pragma warning restore CA1823 // Avoid unused private fields
    private readonly ISender _mediator = mediator;

    public async Task Consume(ConsumeContext<InitializeData> context)
    {
        InitializeData message = context.Message;

        ResetDataCommand command = _mapper.Map<ResetDataCommand>(message);

        await _mediator.Send(command)
                       .ConfigureAwait(false);
    }
}

public class RequireDataConsumerDefinition : ConsumerDefinition<InitializeDataConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Game.Value.Data.Dependent.Data.Initialize; } }

    public RequireDataConsumerDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.data.data.initialize.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<InitializeDataConsumer> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.game.data.init"
            rmq.Bind(Exchange.Name, x => x.AutoDelete = true);
        }
    }
}