
using AutoMapper;

using MassTransit;

using SFC.Game.Application.Interfaces.Game.Data;
using SFC.Game.Application.Interfaces.Game.Data.Models;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Data;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Messages.Events.Game.Data;

namespace SFC.Game.Infrastructure.Services.Game.Data;

public class GameDataService(
    IMapper mapper,
    IPublishEndpoint publisher,
    IGameStatusRepository gameStatusesRepository,
    IGameTeamStatusRepository gameTeamStatusesRepository,
    IGamePlayerStatusRepository gamePlayerStatusesRepository,
    IGameTeamIndexRepository gameTeamIndexRepository) : IGameDataService
{
    private readonly IMapper _mapper = mapper;
    private readonly IPublishEndpoint _publisher = publisher;
    private readonly IGameStatusRepository _gameStatusesRepository = gameStatusesRepository;
    private readonly IGameTeamStatusRepository _gameTeamStatusesRepository = gameTeamStatusesRepository;
    private readonly IGamePlayerStatusRepository _gamePlayerStatusesRepository = gamePlayerStatusesRepository;
    private readonly IGameTeamIndexRepository _gameTeamIndexRepository = gameTeamIndexRepository;

    public async Task<GetAllGameDataModel> GetAllGameDataAsync()
    {
        return new GetAllGameDataModel()
        {
            GameStatuses = await _gameStatusesRepository.ListAllAsync().ConfigureAwait(false),
            GameTeamStatuses = await _gameTeamStatusesRepository.ListAllAsync().ConfigureAwait(false),
            GamePlayerStatuses = await _gamePlayerStatusesRepository.ListAllAsync().ConfigureAwait(false),
            GameTeamIndexes = await _gameTeamIndexRepository.ListAllAsync().ConfigureAwait(false)
        };
    }

    public async Task<GetInviteDataModel> GetInviteDataAsync()
    {
        return new()
        {
            GameStatuses = await _gameStatusesRepository.ListAllAsync().ConfigureAwait(false),
            GamePlayerStatuses = await _gamePlayerStatusesRepository.ListAllAsync().ConfigureAwait(false),
            GameTeamStatuses = await _gameTeamStatusesRepository.ListAllAsync().ConfigureAwait(false),
            GameTeamIndexes = await _gameTeamIndexRepository.ListAllAsync().ConfigureAwait(false)
        };
    }

    public async Task<GetRequestDataModel> GetRequestDataAsync()
    {
        return new()
        {
            GameStatuses = await _gameStatusesRepository.ListAllAsync().ConfigureAwait(false),
            GamePlayerStatuses = await _gamePlayerStatusesRepository.ListAllAsync().ConfigureAwait(false),
            GameTeamStatuses = await _gameTeamStatusesRepository.ListAllAsync().ConfigureAwait(false),
            GameTeamIndexes = await _gameTeamIndexRepository.ListAllAsync().ConfigureAwait(false)
        };
    }

    public async Task<GetSchemeDataModel> GetSchemeDataAsync()
    {
        return new()
        {
            GameStatuses = await _gameStatusesRepository.ListAllAsync().ConfigureAwait(false),
            GameTeamStatuses = await _gameTeamStatusesRepository.ListAllAsync().ConfigureAwait(false),
            GameTeamIndexes = await _gameTeamIndexRepository.ListAllAsync().ConfigureAwait(false)
        };
    }

    public async Task PublishDataInitializedEventAsync(CancellationToken cancellationToken)
    {
        GetAllGameDataModel model = await GetAllGameDataAsync().ConfigureAwait(true);

        DataInitialized @event = _mapper.BuildGameDataInitializedEvent(model);

        await _publisher.Publish(@event, cancellationToken)
                        .ConfigureAwait(false);
    }
}