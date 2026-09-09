using MassTransit;

using Microsoft.Extensions.Configuration;

using SFC.Game.Application.Interfaces.Identity;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Identity.Messages.Commands.User;

namespace SFC.Game.Infrastructure.Services.Identity;
public class UserSeedService(IBus bus, IConfiguration configuration) : IUserSeedService
{
    private readonly IBus _bus = bus;
    private readonly IConfiguration _configuration = configuration;

    public async Task SendRequireUsersSeedAsync(CancellationToken cancellationToken = default)
    {
        RabbitMqSettings settings = _configuration.GetRabbitMqSettings();

        RequireUsersSeed command = new() { Initiator = settings.Exchanges.Game.Key };

        await _bus.Send(command, cancellationToken)
                  .ConfigureAwait(false);
    }
}