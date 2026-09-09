using AutoMapper;

using MassTransit;

using SFC.Game.Application.Interfaces.Game.Player;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Messages.Events.Game.Player;

namespace SFC.Game.Infrastructure.Services.Game.Player;
public class GamePlayerService(IMapper mapper, IPublishEndpoint publisher) : IGamePlayerService
{
    private readonly IPublishEndpoint _publisher = publisher;
    private readonly IMapper _mapper = mapper;

    public Task NotifyGamePlayerCreatedAsync(GamePlayer gamePlayer, CancellationToken cancellationToken = default)
    {
        GamePlayerCreated @event = _mapper.Map<GamePlayerCreated>(gamePlayer);
        return _publisher.Publish(@event, cancellationToken);
    }

    public Task NotifyGamePlayerUpdatedAsync(GamePlayer gamePlayer, CancellationToken cancellationToken = default)
    {
        GamePlayerUpdated @event = _mapper.Map<GamePlayerUpdated>(gamePlayer);
        return _publisher.Publish(@event, cancellationToken);
    }
}