using AutoMapper;

using MassTransit;

using SFC.Game.Application.Interfaces.Common;
using SFC.Game.Application.Interfaces.Game.Player;
using SFC.Game.Application.Interfaces.Metadata;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Player;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Messages.Events.Game.Player;

namespace SFC.Game.Infrastructure.Services.Game.Player;

public class GamePlayerSeedService(
    IMapper mapper,
    IPublishEndpoint publisher,
    IDateTimeService dateTimeService,
    IMetadataService metadataService,
    IGamePlayerRepository gamePlayerRepository,
    IGameRepository gameRepository) : IGamePlayerSeedService
{
    private readonly IMapper _mapper = mapper;
    private readonly IPublishEndpoint _publisher = publisher;
    private readonly IDateTimeService _dateTimeService = dateTimeService;
    private readonly IMetadataService _metadataService = metadataService;
    private readonly IGamePlayerRepository _gamePlayerRepository = gamePlayerRepository;
    private readonly IGameRepository _gameRepository = gameRepository;

    #region Stub data

    private static readonly IEnumerable<(long, GamePlayerStatusEnum, long)> GAME_PLAYERS =
    [
        (1, GamePlayerStatusEnum.InGame, 20),
        (1, GamePlayerStatusEnum.InGame, 21),
        (1, GamePlayerStatusEnum.InGame, 22),
        (2, GamePlayerStatusEnum.OutOfGame, 23),
        (2, GamePlayerStatusEnum.OutOfGame, 24),
        (2, GamePlayerStatusEnum.OutOfGame, 25),
        (3, GamePlayerStatusEnum.InGame, 26),
        (3, GamePlayerStatusEnum.InGame, 27),
        (3, GamePlayerStatusEnum.InGame, 28),
        (3, GamePlayerStatusEnum.OutOfGame, 29),
        (3, GamePlayerStatusEnum.OutOfGame, 30)
    ];

    #endregion Stub data

    #region Public

    public async Task<IEnumerable<GamePlayer>> GetSeedGamePlayersAsync()
    {
        return await _gamePlayerRepository.GetByIdsAsync(GAME_PLAYERS.Select(item => item.Item1), GAME_PLAYERS.Select(item => item.Item3)).ConfigureAwait(true);
    }

    public async Task SeedGamePlayersAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<GamePlayer> gamePlayers = await CreateSeedGamePlayersAsync().ConfigureAwait(true);

        GamePlayer[] seedGamePlayers = await _gamePlayerRepository.AddRangeIfNotExistsAsync([.. gamePlayers]).ConfigureAwait(true);

        await PublishGamePlayersSeededEventAsync(seedGamePlayers, cancellationToken).ConfigureAwait(true);

        await _metadataService.CompleteAsync(MetadataServiceEnum.Game, MetadataDomainEnum.GamePlayer, MetadataTypeEnum.Seed).ConfigureAwait(true);
    }

    #endregion Public

    #region Private

    private async Task<IEnumerable<GamePlayer>> CreateSeedGamePlayersAsync()
    {
        List<GamePlayer> result = [];

        foreach ((long, GamePlayerStatusEnum, long) item in GAME_PLAYERS)
        {
            GamePlayer part = await BuildGamePlayerAsync(item.Item1, item.Item2, item.Item3).ConfigureAwait(true);
            result.Add(part);
        }

        return result;
    }

    private async Task<GamePlayer> BuildGamePlayerAsync(long gameId, GamePlayerStatusEnum status, long playerId)
    {
        GameEntity? game = await _gameRepository.GetByIdAsync(gameId).ConfigureAwait(true);

        Guid userId = game!.UserId;

        DateTime createdDate = _dateTimeService.Now;

        return new GamePlayer()
        {
            CreatedBy = userId,
            CreatedDate = createdDate,
            LastModifiedBy = userId,
            LastModifiedDate = createdDate,
            UserId = userId,
            PlayerId = playerId,
            GameId = gameId,
            StatusId = status
        };
    }

    private Task PublishGamePlayersSeededEventAsync(IEnumerable<GamePlayer> GamePlayers, CancellationToken cancellationToken = default)
    {
        GamePlayersSeeded @event = _mapper.Map<GamePlayersSeeded>(GamePlayers);
        return _publisher.Publish(@event, cancellationToken);
    }

    #endregion Private
}