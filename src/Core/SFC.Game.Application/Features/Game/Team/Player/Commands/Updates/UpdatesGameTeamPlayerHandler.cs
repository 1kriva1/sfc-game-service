using AutoMapper;

using MediatR;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Events.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Updates;

public class UpdatesGameTeamPlayerHandler(IMapper mapper, IGameTeamPlayerRepository gameTeamPlayerRepository)
    : IRequestHandler<UpdatesGameTeamPlayerCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamPlayerRepository _gameTeamPlayerRepository = gameTeamPlayerRepository;

    public async Task Handle(UpdatesGameTeamPlayerCommand request, CancellationToken cancellationToken)
    {
        long[] gameIds = [.. request.GameTeamPlayers.Select(gt => gt.GameId)];
        long[] teamIds = [.. request.GameTeamPlayers.Select(gt => gt.TeamId)];
        long[] playerIds = [.. request.GameTeamPlayers.Select(gt => gt.PlayerId)];

        IEnumerable<GameTeamPlayer> existingGameTeamPlayerss = await _gameTeamPlayerRepository
            .GetByIdsAsync(gameIds, teamIds, playerIds)
            .ConfigureAwait(false);

        if (!existingGameTeamPlayerss.Any())
        {
            throw new NotFoundException(Localization.GameTeamPlayersNotFound);
        }

        IEnumerable<GameTeamPlayer> updatedGameTeamPlayers = request.GameTeamPlayers
            .Join(existingGameTeamPlayerss,
                dto => dto.PlayerId,
                entity => entity.PlayerId,
                (dto, entity) => _mapper.Map(dto, entity));

        foreach (GameTeamPlayer updatedGameTeamPlayer in updatedGameTeamPlayers)
        {
            updatedGameTeamPlayer.AddDomainEvent(new GameTeamPlayerUpdatedEvent(updatedGameTeamPlayer));
        }

        await _gameTeamPlayerRepository.UpdateRangeAsync([.. updatedGameTeamPlayers])
                                       .ConfigureAwait(false);
    }
}