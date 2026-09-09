using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SFC.Game.Application.Features.Team.General.Commands.CreateRange;
using SFC.Game.Application.Interfaces.Metadata;
using SFC.Game.Application.Interfaces.Persistence.Repository.Team.General;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Team.Messages.Events.Team.General;

namespace SFC.Game.Infrastructure.Consumers.Team.Domain.Team.Seed;
public class TeamsSeededConsumer(
    IMapper mapper,
    IWebHostEnvironment environment,
    ILogger<TeamsSeededConsumer> logger,
    ISender mediator,
    IMetadataService metadataService) : IConsumer<TeamsSeeded>
{
#pragma warning disable CA1823 // Avoid unused private fields
    private readonly IMapper _mapper = mapper;
    private readonly IWebHostEnvironment _environment = environment;
    private readonly ILogger<TeamsSeededConsumer> _logger = logger;
    private readonly ISender _mediator = mediator;
    private readonly IMetadataService _metadataService = metadataService;
#pragma warning restore CA1823 // Avoid unused private fields

    public async Task Consume(ConsumeContext<TeamsSeeded> context)
    {
        if (_environment.IsDevelopment())
        {
            if (await _metadataService.IsCompletedAsync(MetadataServiceEnum.Data, MetadataDomainEnum.Data, MetadataTypeEnum.Initialization).ConfigureAwait(true) &&
                await _metadataService.IsCompletedAsync(MetadataServiceEnum.Identity, MetadataDomainEnum.User, MetadataTypeEnum.Seed).ConfigureAwait(true))
            {
                TeamsSeeded @event = context.Message;

                CreateTeamsCommand command = _mapper.Map<CreateTeamsCommand>(@event.Teams);

                await _mediator.Send(command)
                               .ConfigureAwait(false);
            }
        }
    }
}

public class TeamsSeededConsumerDefinition : ConsumerDefinition<TeamsSeededConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Team.Value.Domain.Team.Seed.Seeded; } }

    public TeamsSeededConsumerDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.team.teams.seeded.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<TeamsSeededConsumer> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.team.teams.seeded"
            rmq.Bind(Exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.ExchangeType = Exchange.Type;
            });
        }
    }
}