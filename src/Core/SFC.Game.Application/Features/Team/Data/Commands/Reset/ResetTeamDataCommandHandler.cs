using AutoMapper;

using MediatR;

using SFC.Game.Application.Interfaces.Persistence.Repository.Team.Data;
using SFC.Game.Domain.Entities.Team.Data;
using SFC.Game.Domain.Events.Team.Data;

namespace SFC.Game.Application.Features.Team.Data.Commands.Reset;

public class ResetTeamDataCommandHandler(
    IMapper mapper,
    IMediator mediator,
    ITeamPlayerStatusRepository teamPlayerStatusRepository) : IRequestHandler<ResetTeamDataCommand>
{
#pragma warning disable CA1823 // Avoid unused private fields
    private readonly IMapper _mapper = mapper;
#pragma warning restore CA1823 // Avoid unused private fields
    private readonly IMediator _mediator = mediator;
    private readonly ITeamPlayerStatusRepository _teamPlayerStatusRepository = teamPlayerStatusRepository;

    public async Task Handle(ResetTeamDataCommand request, CancellationToken cancellationToken)
    {
        TeamPlayerStatus[] teamPlayerStatuses = await _teamPlayerStatusRepository
            .ResetAsync(_mapper.Map<IEnumerable<TeamPlayerStatus>>(request.TeamPlayerStatuses))
            .ConfigureAwait(false);

        await PublishDataResetedEventAsync(teamPlayerStatuses, cancellationToken).ConfigureAwait(false);
    }

    private Task PublishDataResetedEventAsync(TeamPlayerStatus[] teamPlayerStatuses, CancellationToken cancellationToken)
    {
        TeamDataResetedEvent @event = new(teamPlayerStatuses);
        return _mediator.Publish(@event, cancellationToken);
    }
}