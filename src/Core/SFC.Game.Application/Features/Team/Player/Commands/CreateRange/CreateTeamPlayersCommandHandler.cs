using AutoMapper;

using MediatR;

using SFC.Game.Application.Interfaces.Persistence.Repository.Team.Player;
using SFC.Game.Domain.Entities.Team.Player;
using SFC.Game.Domain.Events.Team.Player;

namespace SFC.Game.Application.Features.Team.Player.Commands.CreateRange;

public class CreateTeamPlayersCommandHandler(
    IMapper mapper,
    IMediator mediator,
    ITeamPlayerRepository teamPlayerRepository) : IRequestHandler<CreateTeamPlayersCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;
    private readonly ITeamPlayerRepository _teamPlayerRepository = teamPlayerRepository;

    public async Task Handle(CreateTeamPlayersCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<TeamPlayer> teamPlayers = _mapper.Map<IEnumerable<TeamPlayer>>(request.TeamPlayers);

        await _teamPlayerRepository.AddRangeIfNotExistsAsync([.. teamPlayers])
                                   .ConfigureAwait(false);

        TeamPlayersCreatedEvent @event = new(teamPlayers);

        await _mediator.Publish(@event, cancellationToken)
                       .ConfigureAwait(false);
    }
}