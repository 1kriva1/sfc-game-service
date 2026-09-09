using AutoMapper;

using MassTransit;

using SFC.Game.Application.Interfaces.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Messages.Events.Game.Team.General;

namespace SFC.Game.Infrastructure.Services.Game.Team.General;
public class GameTeamService(IMapper mapper, IPublishEndpoint publisher) : IGameTeamService
{
    private readonly IPublishEndpoint _publisher = publisher;
    private readonly IMapper _mapper = mapper;

    public Task NotifyGameTeamCreatedAsync(GameTeam gameTeam, CancellationToken cancellationToken = default)
    {
        GameTeamCreated @event = _mapper.Map<GameTeamCreated>(gameTeam);
        return _publisher.Publish(@event, cancellationToken);
    }

    public Task NotifyGameTeamUpdatedAsync(GameTeam gameTeam, CancellationToken cancellationToken = default)
    {
        GameTeamUpdated @event = _mapper.Map<GameTeamUpdated>(gameTeam);
        return _publisher.Publish(@event, cancellationToken);
    }
}