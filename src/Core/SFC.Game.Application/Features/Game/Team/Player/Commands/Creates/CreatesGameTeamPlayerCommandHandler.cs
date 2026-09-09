using AutoMapper;

using MediatR;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Features.Game.Team.Player.Common.Extensions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Events.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Creates;
public class CreatesGameTeamPlayerCommandHandler(
    IMapper mapper,
    IGameTeamRepository gameTeamRepository,
    IGameTeamPlayerRepository gameTeamPlayerRepository) : IRequestHandler<CreatesGameTeamPlayerCommand, CreatesGameTeamPlayerViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamRepository _gameTeamRepository = gameTeamRepository;
    private readonly IGameTeamPlayerRepository _gameTeamPlayerRepository = gameTeamPlayerRepository;

    public async Task<CreatesGameTeamPlayerViewModel> Handle(CreatesGameTeamPlayerCommand request, CancellationToken cancellationToken)
    {
        long[] gameIds = [.. request.GameTeamPlayers.Select(gt => gt.GameId)];
        long[] teamIds = [.. request.GameTeamPlayers.Select(gt => gt.TeamId)];

        IEnumerable<GameTeam> gameTeams = await _gameTeamRepository.GetByIdsAsync(gameIds, teamIds).ConfigureAwait(true);

        IList<GameTeamPlayer> gameTeamPlayers = [];

        foreach (CreatesGameTeamPlayerDto item in request.GameTeamPlayers)
        {
            GameTeam? gameTeam = gameTeams.FirstOrDefault(x => x.GameId == item.GameId && x.TeamId == item.TeamId);

            if (gameTeam != null)
            {
                GameTeamPlayer gameTeamPlayer = _mapper.Map<GameTeamPlayer>(item)
                                                       .SetGameTeamId(gameTeam.Id)
                                                       .SetStatus(TeamPlayerStatusEnum.Active);

                gameTeamPlayer.AddDomainEvent(new GameTeamPlayerCreatedEvent(gameTeamPlayer));

                gameTeamPlayers.Add(gameTeamPlayer);
            }
        }

        await _gameTeamPlayerRepository.AddRangeIfNotExistsAsync([.. gameTeamPlayers])
                                       .ConfigureAwait(false);

        return _mapper.Map<CreatesGameTeamPlayerViewModel>(gameTeamPlayers);
    }
}