using AutoMapper;

using MassTransit;

using SFC.Game.Application.Interfaces.Game.Team.Player;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Messages.Events.Game.Team.Player;

namespace SFC.Game.Infrastructure.Services.Game.Team.General;
public class GameTeamPlayerService(IMapper mapper, IPublishEndpoint publisher) : IGameTeamPlayerService
{
    private readonly IPublishEndpoint _publisher = publisher;
    private readonly IMapper _mapper = mapper;

    public Task NotifyGameTeamPlayerCreatedAsync(GameTeamPlayer gameTeamPlayer, CancellationToken cancellationToken = default)
    {
        GameTeamPlayerCreated @event = _mapper.Map<GameTeamPlayerCreated>(gameTeamPlayer);
        return _publisher.Publish(@event, cancellationToken);
    }

    public Task NotifyGameTeamPlayerUpdatedAsync(GameTeamPlayer gameTeamPlayer, CancellationToken cancellationToken = default)
    {
        GameTeamPlayerUpdated @event = _mapper.Map<GameTeamPlayerUpdated>(gameTeamPlayer);
        return _publisher.Publish(@event, cancellationToken);
    }
}