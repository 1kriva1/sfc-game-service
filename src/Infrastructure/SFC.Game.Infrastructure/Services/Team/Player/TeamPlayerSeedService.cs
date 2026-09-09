using MassTransit;

using Microsoft.Extensions.Configuration;

using SFC.Game.Application.Interfaces.Team.Player;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Team.Messages.Commands.Team.Player;

namespace SFC.Game.Infrastructure.Services.Team.Player;
public class TeamPlayerSeedService(IConfiguration configuration, IBus bus) : ITeamPlayerSeedService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IBus _bus = bus;

    public async Task SendRequireTeamPlayersSeedAsync(CancellationToken cancellationToken = default)
    {
        RabbitMqSettings settings = _configuration.GetRabbitMqSettings();

        RequireTeamPlayersSeed command = new() { Initiator = settings.Exchanges.Game.Key };

        await _bus.Send(command, cancellationToken)
                  .ConfigureAwait(false);
    }
}