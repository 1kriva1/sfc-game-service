using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SFC.Game.Application.Features.Team.General.Commands.Create;
using SFC.Game.Application.Interfaces.Persistence.Repository.Team.General;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Team.Messages.Events.Team.General;

namespace SFC.Game.Infrastructure.Consumers.Team.Domain.Team.Events;
public class TeamCreatedConsumer(
    IMapper mapper,
    ILogger<TeamCreatedConsumer> logger,
    ISender mediator,
    ITeamRepository teamRepository) : IConsumer<TeamCreated>
{
#pragma warning disable CA1823 // Avoid unused private fields
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<TeamCreatedConsumer> _logger = logger;
    private readonly ISender _mediator = mediator;
    private readonly ITeamRepository _teamRepository = teamRepository;
#pragma warning restore CA1823 // Avoid unused private fields

    public async Task Consume(ConsumeContext<TeamCreated> context)
    {
        TeamCreated @event = context.Message;

        bool teamExist = await _teamRepository.AnyAsync(@event.Team.Id)
                                              .ConfigureAwait(true);

        if (!teamExist)
        {
            CreateTeamCommand command = _mapper.Map<CreateTeamCommand>(@event);

            await _mediator.Send(command)
                           .ConfigureAwait(false);
        }
    }
}

public class TeamCreatedConsumerDefinition : ConsumerDefinition<TeamCreatedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Team.Value.Domain.Team.Events.Created; } }

    public TeamCreatedConsumerDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.team.created.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<TeamCreatedConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.team.created"
            rmq.Bind(Exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.ExchangeType = Exchange.Type;
            });
        }
    }
}