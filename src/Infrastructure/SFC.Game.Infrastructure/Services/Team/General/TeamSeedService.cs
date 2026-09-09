using MassTransit;

using Microsoft.Extensions.Configuration;

using SFC.Game.Application.Interfaces.Team.General;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Team.Messages.Commands.Team;
using SFC.Team.Messages.Commands.Team.General;

namespace SFC.Game.Infrastructure.Services.Team.General;
public class TeamSeedService(IConfiguration configuration, IBus bus) : ITeamSeedService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IBus _bus = bus;

    public async Task SendRequireTeamsSeedAsync(CancellationToken cancellationToken = default)
    {
        RabbitMqSettings settings = _configuration.GetRabbitMqSettings();

        RequireTeamsSeed command = new() { Initiator = settings.Exchanges.Game.Key };

        await _bus.Send(command, cancellationToken)
                  .ConfigureAwait(false);
    }
}