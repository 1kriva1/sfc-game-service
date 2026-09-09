using MediatR;

using Microsoft.Extensions.Hosting;

using SFC.Game.Application.Interfaces.Game.General;
using SFC.Game.Application.Interfaces.Game.Player;
using SFC.Game.Application.Interfaces.Game.Team.General;
using SFC.Game.Application.Interfaces.Game.Team.Player;
using SFC.Game.Application.Interfaces.Metadata;
using SFC.Game.Domain.Events.Team.Player;

namespace SFC.Game.Application.Features.Team.Player.Notifications.TeamPlayersCreated;
public class TeamPlayersCreatedNotificationHandler(
    IMetadataService metadataService,
    IHostEnvironment hostEnvironment,
    IGameSeedService gameSeedService,
    IGameTeamSeedService gameTeamSeedService,
    IGamePlayerSeedService gamePlayerSeedService,
    IGameTeamPlayerSeedService gameTeamPlayerSeedService) : INotificationHandler<TeamPlayersCreatedEvent>
{
    private readonly IMetadataService _metadataService = metadataService;
    private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
    private readonly IGameSeedService _gameSeedService = gameSeedService;
    private readonly IGameTeamSeedService _gameTeamSeedService = gameTeamSeedService;
    private readonly IGamePlayerSeedService _gamePlayerSeedService = gamePlayerSeedService;
    private readonly IGameTeamPlayerSeedService _gameTeamPlayerSeedService = gameTeamPlayerSeedService;

    public async Task Handle(TeamPlayersCreatedEvent notification, CancellationToken cancellationToken)
    {
        if (_hostEnvironment.IsDevelopment())
        {
            await _metadataService.CompleteAsync(MetadataServiceEnum.Team, MetadataDomainEnum.TeamPlayer, MetadataTypeEnum.Seed).ConfigureAwait(false);

            if (!await _metadataService.IsCompletedAsync(MetadataServiceEnum.Game, MetadataDomainEnum.Game, MetadataTypeEnum.Seed).ConfigureAwait(false))
            {
                await _gameSeedService.SeedGamesAsync(cancellationToken).ConfigureAwait(false);
            }

            if (!await _metadataService.IsCompletedAsync(MetadataServiceEnum.Game, MetadataDomainEnum.GamePlayer, MetadataTypeEnum.Seed).ConfigureAwait(false))
            {
                await _gamePlayerSeedService.SeedGamePlayersAsync(cancellationToken).ConfigureAwait(false);
            }

            if (!await _metadataService.IsCompletedAsync(MetadataServiceEnum.Game, MetadataDomainEnum.GameTeam, MetadataTypeEnum.Seed).ConfigureAwait(false))
            {
                await _gameTeamSeedService.SeedGameTeamsAsync(cancellationToken).ConfigureAwait(false);
            }

            if (!await _metadataService.IsCompletedAsync(MetadataServiceEnum.Game, MetadataDomainEnum.GameTeamPlayer, MetadataTypeEnum.Seed).ConfigureAwait(false))
            {
                await _gameTeamPlayerSeedService.SeedGameTeamPlayersAsync(cancellationToken).ConfigureAwait(false);
            }
        }
    }
}