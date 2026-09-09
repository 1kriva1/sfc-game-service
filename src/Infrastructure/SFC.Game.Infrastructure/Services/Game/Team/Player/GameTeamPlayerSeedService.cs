using AutoMapper;

using MassTransit;

using SFC.Game.Application.Interfaces.Common;
using SFC.Game.Application.Interfaces.Game.Team.Player;
using SFC.Game.Application.Interfaces.Metadata;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Enums.Team;
using SFC.Game.Messages.Events.Game.Team.Player;

namespace SFC.Game.Infrastructure.Services.Game.Team.Player;

public class GameTeamPlayerSeedService(
    IMapper mapper,
    IPublishEndpoint publisher,
    IDateTimeService dateTimeService,
    IMetadataService metadataService,
    IGameTeamPlayerRepository gameTeamPlayerRepository,
    IGameTeamRepository gameTeamRepository) : IGameTeamPlayerSeedService
{
    private readonly IMapper _mapper = mapper;
    private readonly IPublishEndpoint _publisher = publisher;
    private readonly IDateTimeService _dateTimeService = dateTimeService;
    private readonly IMetadataService _metadataService = metadataService;
    private readonly IGameTeamPlayerRepository _gameTeamPlayerRepository = gameTeamPlayerRepository;
    private readonly IGameTeamRepository _gameTeamRepository = gameTeamRepository;

    #region Stub data

    private static readonly IEnumerable<(long, long, long, TeamPlayerStatus)> GAME_TEAM_PLAYERS =
    [
        (1, 20, 20, TeamPlayerStatus.Active),
        (1, 21, 21, TeamPlayerStatus.Active),
        (2, 23, 22, TeamPlayerStatus.Active),
        (4, 29, 25, TeamPlayerStatus.Active)
    ];

    #endregion Stub data

    #region Public

    public async Task<IEnumerable<GameTeamPlayer>> GetSeedGameTeamPlayersAsync()
    {
        return await _gameTeamPlayerRepository.GetByIdsAsync(GAME_TEAM_PLAYERS.Select(item => item.Item1), GAME_TEAM_PLAYERS.Select(item => item.Item2), GAME_TEAM_PLAYERS.Select(item => item.Item3)).ConfigureAwait(true);
    }

    public async Task SeedGameTeamPlayersAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<GameTeamPlayer> gameTeamPlayers = await CreateSeedGameTeamPlayersAsync().ConfigureAwait(true);

        GameTeamPlayer[] seedGameTeamPlayers = await _gameTeamPlayerRepository.AddRangeIfNotExistsAsync([.. gameTeamPlayers]).ConfigureAwait(true);

        await PublishGameTeamPlayersSeededEventAsync(seedGameTeamPlayers, cancellationToken).ConfigureAwait(true);

        await _metadataService.CompleteAsync(MetadataServiceEnum.Game, MetadataDomainEnum.GameTeamPlayer, MetadataTypeEnum.Seed).ConfigureAwait(true);
    }

    #endregion Public

    #region Private

    private async Task<IEnumerable<GameTeamPlayer>> CreateSeedGameTeamPlayersAsync()
    {
        List<GameTeamPlayer> result = [];

        foreach ((long, long, long, TeamPlayerStatus) item in GAME_TEAM_PLAYERS)
        {
            GameTeamPlayer part = await BuildGameTeamPlayerAsync(item.Item1, item.Item2, item.Item3, item.Item4).ConfigureAwait(true);
            result.Add(part);
        }

        return result;
    }

    private async Task<GameTeamPlayer> BuildGameTeamPlayerAsync(long gameId, long teamId, long playerId, TeamPlayerStatus status)
    {
        GameTeam? gameTeam = await _gameTeamRepository.GetByIdAsync(gameId, teamId).ConfigureAwait(true);

        Guid userId = gameTeam!.UserId;

        DateTime createdDate = _dateTimeService.Now;

        return new GameTeamPlayer()
        {
            CreatedBy = userId,
            CreatedDate = createdDate,
            LastModifiedBy = userId,
            LastModifiedDate = createdDate,
            GameTeamId = gameTeam!.Id,
            UserId = userId,
            PlayerId = playerId,
            StatusId = status
        };
    }

    private Task PublishGameTeamPlayersSeededEventAsync(IEnumerable<GameTeamPlayer> gameTeamPlayers, CancellationToken cancellationToken = default)
    {
        GameTeamPlayersSeeded @event = _mapper.Map<GameTeamPlayersSeeded>(gameTeamPlayers);
        return _publisher.Publish(@event, cancellationToken);
    }

    #endregion Private
}