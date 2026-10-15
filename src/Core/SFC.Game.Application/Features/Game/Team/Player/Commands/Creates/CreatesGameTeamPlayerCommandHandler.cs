using AutoMapper;

using MediatR;

using SFC.Game.Application.Features.Game.Team.Player.Common.Extensions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Player;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team.General;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team.Player;
using SFC.Game.Application.Interfaces.Persistence.Repository.Team.Player;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Entities.Team.Player;
using SFC.Game.Domain.Events.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Creates;
public class CreatesGameTeamPlayerCommandHandler(
    IMapper mapper,
    IGameTeamRepository gameTeamRepository,
    IGamePlayerRepository gamePlayerRepository,
    ITeamPlayerRepository teamPlayerRepository,
    IGameTeamPlayerRepository gameTeamPlayerRepository) : IRequestHandler<CreatesGameTeamPlayerCommand, CreatesGameTeamPlayerViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamRepository _gameTeamRepository = gameTeamRepository;
    private readonly IGamePlayerRepository _gamePlayerRepository = gamePlayerRepository;
    private readonly ITeamPlayerRepository _teamPlayerRepository = teamPlayerRepository;
    private readonly IGameTeamPlayerRepository _gameTeamPlayerRepository = gameTeamPlayerRepository;

    public async Task<CreatesGameTeamPlayerViewModel> Handle(CreatesGameTeamPlayerCommand request, CancellationToken cancellationToken)
    {
        long[] gameIds = [.. request.GameTeamPlayers.Select(gt => gt.GameId)];
        long[] teamIds = [.. request.GameTeamPlayers.Select(gt => gt.TeamId)];
        long[] playerIds = [.. request.GameTeamPlayers.Select(gt => gt.PlayerId)];

        IEnumerable<GameTeam> gameTeams = await _gameTeamRepository.GetByIdsAsync(gameIds, teamIds).ConfigureAwait(true);

        IReadOnlyList<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByIdsAsync(gameIds, playerIds).ConfigureAwait(true);

        IReadOnlyList<TeamPlayer> teamPlayers = await _teamPlayerRepository.GetByIdsAsync(teamIds, playerIds).ConfigureAwait(true);

        IList<GameTeamPlayer> gameTeamPlayers = [];

        foreach (CreatesGameTeamPlayerDto item in request.GameTeamPlayers)
        {
            GameTeam? gameTeam = gameTeams.FirstOrDefault(x => x.GameId == item.GameId && x.TeamId == item.TeamId);

            if (gameTeam != null)
            {
                GamePlayer? gamePlayer = gamePlayers.FirstOrDefault(x => x.GameId == item.GameId && x.PlayerId == item.PlayerId);

                TeamPlayer? teamPlayer = teamPlayers.FirstOrDefault(x => x.TeamId == item.TeamId && x.PlayerId == item.PlayerId);

                if (gamePlayer != null || teamPlayer != null)
                {
                    GameTeamPlayer gameTeamPlayer = _mapper.Map<GameTeamPlayer>(item)
                                                       .SetGameTeamId(gameTeam.Id)
                                                       .SetGamePlayerId(gamePlayer?.Id)
                                                       .SetTeamPlayerId(teamPlayer?.Id);

                    gameTeamPlayer.AddDomainEvent(new GameTeamPlayerCreatedEvent(gameTeamPlayer));

                    gameTeamPlayers.Add(gameTeamPlayer);
                }
            }
        }

        await _gameTeamPlayerRepository.AddRangeIfNotExistsAsync([.. gameTeamPlayers])
                                       .ConfigureAwait(false);

        IEnumerable<long?> gamePlayerIds = gameTeamPlayers.Select(item => item.GamePlayerId);
        IEnumerable<long?> teamPlayerIds = gameTeamPlayers.Select(item => item.TeamPlayerId);

        IReadOnlyList<GameTeamPlayer> result = await _gameTeamPlayerRepository
            .GetByIdsAsync(gameIds, teamIds, gamePlayerIds, teamPlayerIds, [GameTeamPlayerIncludeOptions.All])
            .ConfigureAwait(true);

        return _mapper.Map<CreatesGameTeamPlayerViewModel>(result);
    }
}