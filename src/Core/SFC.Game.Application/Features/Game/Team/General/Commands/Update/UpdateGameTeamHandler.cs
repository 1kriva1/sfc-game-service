using AutoMapper;

using MediatR;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Events.Game.Team.General;

namespace SFC.Game.Application.Features.Game.Team.General.Commands.Update;
public class UpdateGameTeamHandler(IMapper mapper, IGameTeamRepository gameTeamRepository)
    : IRequestHandler<UpdateGameTeamCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamRepository _gameTeamRepository = gameTeamRepository;

    public async Task Handle(UpdateGameTeamCommand request, CancellationToken cancellationToken)
    {
        GameTeam gameTeam = await _gameTeamRepository
            .GetByIdAsync(request.GameTeam.GameId, request.GameTeam.TeamId).ConfigureAwait(true)
                ?? throw new NotFoundException(Localization.GameTeamNotFound);

        GameTeam updatedGameTeam = _mapper.Map(request.GameTeam, gameTeam);

        updatedGameTeam.AddDomainEvent(new GameTeamUpdatedEvent(updatedGameTeam));

        await _gameTeamRepository.UpdateAsync(updatedGameTeam)
                                 .ConfigureAwait(false);
    }
}