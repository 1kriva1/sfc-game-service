using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SFC.Game.Application.Features.Team.Player.Commands.CreateRange;
using SFC.Game.Application.Interfaces.Metadata;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Game.Messages.Events.Game.Team;
using SFC.Team.Messages.Events.Team.Player;

namespace SFC.Game.Infrastructure.Consumers.Team.Domain.Player.Seed;
public class TeamPlayersSeededConsumer(
    IMapper mapper,
    IWebHostEnvironment environment,
    ILogger<TeamPlayersSeededConsumer> logger,
    ISender mediator,
    IMetadataService metadataService) : IConsumer<TeamPlayersSeeded>
{
#pragma warning disable CA1823 // Avoid unused private fields
    private readonly IMapper _mapper = mapper;
    private readonly IWebHostEnvironment _environment = environment;
    private readonly ILogger<TeamPlayersSeededConsumer> _logger = logger;
    private readonly ISender _mediator = mediator;
    private readonly IMetadataService _metadataService = metadataService;
#pragma warning restore CA1823 // Avoid unused private fields

    public async Task Consume(ConsumeContext<TeamPlayersSeeded> context)
    {
        if (_environment.IsDevelopment())
        {
            if (await _metadataService.IsCompletedAsync(MetadataServiceEnum.Team, MetadataDomainEnum.Team, MetadataTypeEnum.Seed).ConfigureAwait(true) &&
                await _metadataService.IsCompletedAsync(MetadataServiceEnum.Player, MetadataDomainEnum.Player, MetadataTypeEnum.Seed).ConfigureAwait(true))
            {
                TeamPlayersSeeded @event = context.Message;

                CreateTeamPlayersCommand command = _mapper.Map<CreateTeamPlayersCommand>(@event.TeamPlayers);

                await _mediator.Send(command)
                               .ConfigureAwait(false);
            }
        }
    }
}

public class TeamPlayersSeededConsumerDefinition : ConsumerDefinition<TeamPlayersSeededConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Team.Value.Domain.Player.Seed.Seeded; } }

    public TeamPlayersSeededConsumerDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.team.players.seeded.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<TeamPlayersSeededConsumer> consumerConfigurator, IRegistrationContext context)
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