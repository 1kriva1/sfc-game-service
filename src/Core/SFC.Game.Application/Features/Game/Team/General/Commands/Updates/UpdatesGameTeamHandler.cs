using AutoMapper;

using MediatR;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Events.Game.Team.General;

namespace SFC.Game.Application.Features.Game.Team.General.Commands.Updates;

public class UpdatesGameTeamHandler(IMapper mapper, IGameTeamRepository gameTeamRepository)
    : IRequestHandler<UpdatesGameTeamCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamRepository _gameTeamRepository = gameTeamRepository;

    public async Task Handle(UpdatesGameTeamCommand request, CancellationToken cancellationToken)
    {
        long[] gameIds = [.. request.GameTeams.Select(gt => gt.GameId)];
        long[] teamIds = [.. request.GameTeams.Select(gt => gt.TeamId)];

        IEnumerable<GameTeam> existingGameTeams = await _gameTeamRepository
            .GetByIdsAsync(gameIds, teamIds)
            .ConfigureAwait(false);

        if (!existingGameTeams.Any())
        {
            throw new NotFoundException(Localization.GameTeamsNotFound);
        }

        IEnumerable<GameTeam> updatedGameTeams = request.GameTeams
            .Join(existingGameTeams,
                dto => (dto.GameId, dto.TeamId),
                entity => (entity.GameId, entity.TeamId),
                (dto, entity) => _mapper.Map(dto, entity));

        foreach (GameTeam updatedGameTeam in updatedGameTeams)
        {
            updatedGameTeam.AddDomainEvent(new GameTeamUpdatedEvent(updatedGameTeam));
        }

        await _gameTeamRepository.UpdateRangeAsync([.. updatedGameTeams])
                                 .ConfigureAwait(false);
    }
}