using AutoMapper;

using MassTransit;

using SFC.Game.Application.Interfaces.Game.General;
using SFC.Game.Messages.Events.Game.General;

namespace SFC.Game.Infrastructure.Services.Game.General;
public class GameService(IMapper mapper, IPublishEndpoint publisher) : IGameService
{
    private readonly IPublishEndpoint _publisher = publisher;
    private readonly IMapper _mapper = mapper;

    public Task NotifyGameCreatedAsync(GameEntity game, CancellationToken cancellationToken = default)
    {
        GameCreated @event = _mapper.Map<GameCreated>(game);
        return _publisher.Publish(@event, cancellationToken);
    }

    public Task NotifyGameUpdatedAsync(GameEntity game, CancellationToken cancellationToken = default)
    {
        GameUpdated @event = _mapper.Map<GameUpdated>(game);
        return _publisher.Publish(@event, cancellationToken);
    }
}