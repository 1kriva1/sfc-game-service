using MediatR;

using Microsoft.Extensions.Hosting;

using SFC.Game.Application.Interfaces.Metadata;
using SFC.Game.Application.Interfaces.Team.Player;
using SFC.Game.Domain.Events.Team.General;

namespace SFC.Game.Application.Features.Team.General.Notifications.TeamsCreated;
public class TeamsCreatedNotificationHandler(
    IMetadataService metadataService,
    IHostEnvironment hostEnvironment,
    ITeamPlayerSeedService teamPlayerSeedService) : INotificationHandler<TeamsCreatedEvent>
{
    private readonly IMetadataService _metadataService = metadataService;
    private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
    private readonly ITeamPlayerSeedService _teamPlayerSeedService = teamPlayerSeedService;

    public async Task Handle(TeamsCreatedEvent notification, CancellationToken cancellationToken)
    {
        if (_hostEnvironment.IsDevelopment())
        {
            await _metadataService.CompleteAsync(MetadataServiceEnum.Team, MetadataDomainEnum.Team, MetadataTypeEnum.Seed).ConfigureAwait(false);

            if (!await _metadataService.IsCompletedAsync(MetadataServiceEnum.Team, MetadataDomainEnum.TeamPlayer, MetadataTypeEnum.Seed).ConfigureAwait(true))
            {
                if (await _metadataService.IsCompletedAsync(MetadataServiceEnum.Player, MetadataDomainEnum.Player, MetadataTypeEnum.Seed).ConfigureAwait(true))
                {
                    await _teamPlayerSeedService.SendRequireTeamPlayersSeedAsync(cancellationToken)
                                                .ConfigureAwait(false);
                }
            }
        }
    }
}