using AutoMapper;

using MassTransit;

using SFC.Game.Application.Interfaces.Common;
using SFC.Game.Application.Interfaces.Game.Team.General;
using SFC.Game.Application.Interfaces.Metadata;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Enums.Game;
using SFC.Game.Messages.Events.Game.Team.General;

namespace SFC.Game.Infrastructure.Services.Game.Team.General;

public class GameTeamSeedService(
    IMapper mapper,
    IPublishEndpoint publisher,
    IDateTimeService dateTimeService,
    IMetadataService metadataService,
    IGameTeamRepository gameTeamRepository,
    IGameRepository gameRepository) : IGameTeamSeedService
{
    private readonly IMapper _mapper = mapper;
    private readonly IPublishEndpoint _publisher = publisher;
    private readonly IDateTimeService _dateTimeService = dateTimeService;
    private readonly IMetadataService _metadataService = metadataService;
    private readonly IGameTeamRepository _gameTeamRepository = gameTeamRepository;
    private readonly IGameRepository _gameRepository = gameRepository;

    #region Stub data

    private static readonly IEnumerable<(long, GameTeamStatusEnum, long, GameTeamIndex?)> GAME_TEAMS =
    [
        (1, GameTeamStatusEnum.InGame, 20, GameTeamIndex.A),
        (1, GameTeamStatusEnum.InGame, 21, GameTeamIndex.B),
        (1, GameTeamStatusEnum.OutOfGame, 22, null),
        (2, GameTeamStatusEnum.InGame, 23, GameTeamIndex.A),
        (2, GameTeamStatusEnum.OutOfGame, 24, null),
        (3, GameTeamStatusEnum.OutOfGame, 25, null),
        (3, GameTeamStatusEnum.OutOfGame, 26, null),
        (3, GameTeamStatusEnum.OutOfGame, 27, null),
        (4, GameTeamStatusEnum.OutOfGame, 28, null),
        (4, GameTeamStatusEnum.InGame, 29, GameTeamIndex.B),
    ];

    #endregion Stub data

    #region Public

    public async Task<IEnumerable<GameTeam>> GetSeedGameTeamsAsync()
    {
        return await _gameTeamRepository.GetByIdsAsync(GAME_TEAMS.Select(item => item.Item1), GAME_TEAMS.Select(item => item.Item3)).ConfigureAwait(true);
    }

    public async Task SeedGameTeamsAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<GameTeam> gameTeams = await CreateSeedGameTeamsAsync().ConfigureAwait(true);

        GameTeam[] seedGameTeams = await _gameTeamRepository.AddRangeIfNotExistsAsync([.. gameTeams]).ConfigureAwait(true);

        await PublishGameTeamsSeededEventAsync(seedGameTeams, cancellationToken).ConfigureAwait(true);

        await _metadataService.CompleteAsync(MetadataServiceEnum.Game, MetadataDomainEnum.GameTeam, MetadataTypeEnum.Seed).ConfigureAwait(true);
    }

    #endregion Public

    #region Private

    private async Task<IEnumerable<GameTeam>> CreateSeedGameTeamsAsync()
    {
        List<GameTeam> result = [];

        foreach ((long, GameTeamStatusEnum, long, GameTeamIndex?) item in GAME_TEAMS)
        {
            GameTeam part = await BuildGameTeamAsync(item.Item1, item.Item2, item.Item3, item.Item4).ConfigureAwait(true);
            result.Add(part);
        }

        return result;
    }

    private async Task<GameTeam> BuildGameTeamAsync(long gameId, GameTeamStatusEnum status, long teamId, GameTeamIndex? index)
    {
        GameEntity? game = await _gameRepository.GetByIdAsync(gameId).ConfigureAwait(true);

        Guid userId = game!.UserId;

        DateTime createdDate = _dateTimeService.Now;

        return new GameTeam()
        {
            CreatedBy = userId,
            CreatedDate = createdDate,
            LastModifiedBy = userId,
            LastModifiedDate = createdDate,
            UserId = userId,
            TeamId = teamId,
            GameId = gameId,
            StatusId = status,
            Index = index
        };
    }

    private Task PublishGameTeamsSeededEventAsync(IEnumerable<GameTeam> gameTeams, CancellationToken cancellationToken = default)
    {
        GameTeamsSeeded @event = _mapper.Map<GameTeamsSeeded>(gameTeams);
        return _publisher.Publish(@event, cancellationToken);
    }

    #endregion Private
}