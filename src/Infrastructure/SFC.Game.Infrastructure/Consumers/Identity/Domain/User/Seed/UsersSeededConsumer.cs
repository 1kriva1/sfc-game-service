using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SFC.Game.Application.Features.Identity.Commands.CreateRange;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Identity.Messages.Events.User;

namespace SFC.Game.Infrastructure.Consumers.Identity.Domain.User.Seed;
public class UsersSeededConsumer(
    IMapper mapper,
    IWebHostEnvironment environment,
    ILogger<UsersSeededConsumer> logger,
    ISender mediator) : IConsumer<UsersSeeded>
{
#pragma warning disable CA1823 // Avoid unused private fields
    private readonly IMapper _mapper = mapper;
    private readonly IWebHostEnvironment _environment = environment;
    private readonly ILogger<UsersSeededConsumer> _logger = logger;
    private readonly ISender _mediator = mediator;
#pragma warning restore CA1823 // Avoid unused private fields

    public async Task Consume(ConsumeContext<UsersSeeded> context)
    {
        if (_environment.IsDevelopment())
        {
            UsersSeeded @event = context.Message;

            CreateUsersCommand command = _mapper.Map<CreateUsersCommand>(@event.Users);

            await _mediator.Send(command)
                           .ConfigureAwait(false);
        }
    }
}

public class UsersSeededConsumerDefinition : ConsumerDefinition<UsersSeededConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Identity.Value.Domain.User.Seed.Seeded; } }

    public UsersSeededConsumerDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.game.identity.users.seeded.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<UsersSeededConsumer> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.identity.users.seeded"
            rmq.Bind(Exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.ExchangeType = Exchange.Type;
            });
        }
    }
}