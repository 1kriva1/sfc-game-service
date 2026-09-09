using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SFC.Game.Application.Features.Team.General.Commands.Create;
using SFC.Game.Application.Features.Team.General.Commands.Update;
using SFC.Game.Application.Interfaces.Persistence.Repository.Team.General;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Team.Messages.Events.Team.General;

namespace SFC.Game.Infrastructure.Consumers.Team.Domain.Team.Events;
public class TeamUpdatedConsumer(
    IMapper mapper,
    ILogger<TeamUpdatedConsumer> logger,
    ISender mediator,
    ITeamRepository teamRepository) : IConsumer<TeamUpdated>
{
#pragma warning disable CA1823 // Avoid unused private fields
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<TeamUpdatedConsumer> _logger = logger;
    private readonly ISender _mediator = mediator;
    private readonly ITeamRepository _teamRepository = teamRepository;
#pragma warning restore CA1823 // Avoid unused private fields

    public async Task Consume(ConsumeContext<TeamUpdated> context)
    {
        TeamUpdated @event = context.Message;

        bool teamExist = await _teamRepository.AnyAsync(@event.Team.Id)
                                                  .ConfigureAwait(true);

        if (teamExist)
            await UpdateTeamAsync(@event).ConfigureAwait(false);
        else
            await CreateTeamAsync(@event).ConfigureAwait(false);
    }

    private Task UpdateTeamAsync(TeamUpdated @event)
    {
        UpdateTeamCommand command = _mapper.Map<UpdateTeamCommand>(@event);
        return _mediator.Send(command);
    }

    private Task CreateTeamAsync(TeamUpdated @event)
    {
        CreateTeamCommand command = _mapper.Map<CreateTeamCommand>(@event);
        return _mediator.Send(command);
    }
}

public class TeamUpdatedConsumerDefinition : ConsumerDefinition<TeamUpdatedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Team.Value.Domain.Team.Events.Updated; } }

    public TeamUpdatedConsumerDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.team.updated.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<TeamUpdatedConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.team.updated"
            rmq.Bind(Exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.ExchangeType = Exchange.Type;
            });
        }
    }
}