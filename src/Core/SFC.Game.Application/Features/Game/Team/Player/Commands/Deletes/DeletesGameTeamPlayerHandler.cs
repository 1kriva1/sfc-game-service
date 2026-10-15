using MediatR;

using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team.Player;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Events.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Deletes;

public class DeletesGameTeamPlayerHandler(IGameTeamPlayerRepository gameTeamPlayerRepository)
    : IRequestHandler<DeletesGameTeamPlayerCommand>
{
    private readonly IGameTeamPlayerRepository _gameTeamPlayerRepository = gameTeamPlayerRepository;

    public async Task Handle(DeletesGameTeamPlayerCommand request, CancellationToken cancellationToken)
    {
        long[] gameIds = [.. request.GameTeamPlayers.Select(gt => gt.GameId)];
        long[] teamIds = [.. request.GameTeamPlayers.Select(gt => gt.TeamId)];
        long[] playerIds = [.. request.GameTeamPlayers.Select(gt => gt.PlayerId)];

        IEnumerable<GameTeamPlayer> existingGameTeamPlayers = await _gameTeamPlayerRepository
            .GetByIdsAsync(gameIds, teamIds, playerIds)
            .ConfigureAwait(false);

        foreach (GameTeamPlayer gameTeamPlayer in existingGameTeamPlayers)
        {
            gameTeamPlayer.AddDomainEvent(new GameTeamPlayerDeletedEvent(gameTeamPlayer));
        }

        await _gameTeamPlayerRepository.DeleteRangeAsync([.. existingGameTeamPlayers])
                                       .ConfigureAwait(false);
    }
}