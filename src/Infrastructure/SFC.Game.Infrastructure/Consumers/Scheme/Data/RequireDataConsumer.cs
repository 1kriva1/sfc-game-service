using AutoMapper;

using MassTransit;

using Microsoft.Extensions.Configuration;

using SFC.Game.Application.Interfaces.Game.Data;
using SFC.Game.Application.Interfaces.Game.Data.Models;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Scheme.Messages.Commands.Game.Data;

namespace SFC.Game.Infrastructure.Consumers.Scheme.Data;

public class RequireDataConsumer(IMapper mapper, IGameDataService gameDataService)
    : IConsumer<RequireData>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameDataService _gameDataService = gameDataService;

    public async Task Consume(ConsumeContext<RequireData> context)
    {
        GetSchemeDataModel model = await _gameDataService.GetSchemeDataAsync().ConfigureAwait(true);

        InitializeData command = _mapper.BuildInitializeDataCommand(model);

        await context.Send(command).ConfigureAwait(false);
    }
}

public class RequireDataConsumerDefinition : ConsumerDefinition<RequireDataConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Message Exchange { get { return _settings.Exchanges.Scheme.Value.Data.Dependent.Game.RequireInitialize; } }

    public RequireDataConsumerDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.scheme.data.initialize.require.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<RequireDataConsumer> consumerConfigurator,
            IRegistrationContext context)
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