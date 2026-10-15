using AutoMapper;

using MediatR;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
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

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Create;

public class CreateGameTeamPlayerHandler(
    IMapper mapper,
    IGameTeamRepository gameTeamRepository,
    IGamePlayerRepository gamePlayerRepository,
    ITeamPlayerRepository teamPlayerRepository,
    IGameTeamPlayerRepository gameTeamPlayerRepository)
    : IRequestHandler<CreateGameTeamPlayerCommand, CreateGameTeamPlayerViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamRepository _gameTeamRepository = gameTeamRepository;
    private readonly IGamePlayerRepository _gamePlayerRepository = gamePlayerRepository;
    private readonly ITeamPlayerRepository _teamPlayerRepository = teamPlayerRepository;
    private readonly IGameTeamPlayerRepository _gameTeamPlayerRepository = gameTeamPlayerRepository;

    public async Task<CreateGameTeamPlayerViewModel> Handle(CreateGameTeamPlayerCommand request, CancellationToken cancellationToken)
    {
        GameTeam gameTeam = await _gameTeamRepository.GetByIdAsync(request.GameTeamPlayer.GameId, request.GameTeamPlayer.TeamId).ConfigureAwait(true)
            ?? throw new NotFoundException(Localization.GameTeamNotFound);

        GamePlayer? gamePlayer = await _gamePlayerRepository.GetByIdAsync(gameTeam.GameId, request.GameTeamPlayer.PlayerId).ConfigureAwait(true);

        TeamPlayer? teamPlayer = await _teamPlayerRepository.GetByIdAsync(gameTeam.TeamId, request.GameTeamPlayer.PlayerId).ConfigureAwait(true);

        if (gamePlayer == null && teamPlayer == null)
        {
            throw new NotFoundException(Localization.PlayerNotFound);
        }

        GameTeamPlayer gameTeamPlayer = _mapper.Map<GameTeamPlayer>(request.GameTeamPlayer)
                                           .SetGameTeamId(gameTeam.Id)
                                           .SetGamePlayerId(gamePlayer?.Id)
                                           .SetTeamPlayerId(teamPlayer?.Id);

        gameTeamPlayer.AddDomainEvent(new GameTeamPlayerCreatedEvent(gameTeamPlayer));

        await _gameTeamPlayerRepository.AddAsync(gameTeamPlayer)
                                       .ConfigureAwait(true);

        GameTeamPlayer result = await _gameTeamPlayerRepository
            .GetByIdAsync(gameTeam.GameId, gameTeam.TeamId, gameTeamPlayer.GamePlayerId, gameTeamPlayer.TeamPlayerId, [GameTeamPlayerIncludeOptions.All])
            .ConfigureAwait(true) ?? throw new NotFoundException(Localization.GameTeamPlayerNotFound);

        return _mapper.Map<CreateGameTeamPlayerViewModel>(result);
    }
}