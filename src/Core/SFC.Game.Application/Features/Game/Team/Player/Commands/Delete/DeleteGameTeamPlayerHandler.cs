using MediatR;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team.Player;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Events.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Delete;
public class DeleteGameTeamPlayerHandler(IGameTeamPlayerRepository gameTeamPlayerRepository)
    : IRequestHandler<DeleteGameTeamPlayerCommand>
{
    private readonly IGameTeamPlayerRepository _gameTeamPlayerRepository = gameTeamPlayerRepository;

    public async Task Handle(DeleteGameTeamPlayerCommand request, CancellationToken cancellationToken)
    {
        GameTeamPlayer gameTeamPlayer = await _gameTeamPlayerRepository
            .GetByIdAsync(request.GameId, request.TeamId, request.PlayerId).ConfigureAwait(true)
                ?? throw new NotFoundException(Localization.GameTeamPlayerNotFound);

        gameTeamPlayer.AddDomainEvent(new GameTeamPlayerDeletedEvent(gameTeamPlayer));

        await _gameTeamPlayerRepository.DeleteAsync(gameTeamPlayer)
                                       .ConfigureAwait(false);
    }
}