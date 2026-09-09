using MassTransit;

using Microsoft.Extensions.Configuration;

using SFC.Game.Application.Interfaces.Player;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Player.Messages.Commands.Player;

namespace SFC.Game.Infrastructure.Services.Player;
public class PlayerSeedService(IConfiguration configuration, IBus bus) : IPlayerSeedService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IBus _bus = bus;

    public async Task SendRequirePlayersSeedAsync(CancellationToken cancellationToken = default)
    {
        RabbitMqSettings settings = _configuration.GetRabbitMqSettings();

        RequirePlayersSeed command = new() { Initiator = settings.Exchanges.Game.Key };

        await _bus.Send<RequirePlayersSeed>(command, cancellationToken)
                  .ConfigureAwait(false);
    }
}