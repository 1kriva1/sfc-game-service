using AutoMapper;

using MediatR;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Events.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Update;
public class UpdateGameTeamPlayerHandler(IMapper mapper, IGameTeamPlayerRepository gameTeamPlayerRepository)
    : IRequestHandler<UpdateGameTeamPlayerCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamPlayerRepository _gameTeamPlayerRepository = gameTeamPlayerRepository;

    public async Task Handle(UpdateGameTeamPlayerCommand request, CancellationToken cancellationToken)
    {
        GameTeamPlayer gameTeamPlayer = await _gameTeamPlayerRepository
            .GetByIdAsync(request.GameTeamPlayer.GameId, request.GameTeamPlayer.TeamId, request.GameTeamPlayer.PlayerId).ConfigureAwait(true)
                ?? throw new NotFoundException(Localization.GameTeamPlayerNotFound);

        GameTeamPlayer updatedTeamPlayer = _mapper.Map(request.GameTeamPlayer, gameTeamPlayer);

        updatedTeamPlayer.AddDomainEvent(new GameTeamPlayerUpdatedEvent(updatedTeamPlayer));

        await _gameTeamPlayerRepository.UpdateAsync(updatedTeamPlayer)
                                   .ConfigureAwait(false);
    }
}