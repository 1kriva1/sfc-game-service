using AutoMapper;

using MediatR;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Features.Game.Team.Player.Common.Extensions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Events.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Create;

public class CreateGameGameTeamPlayerHandler(
    IMapper mapper,
    IGameTeamRepository gameTeamRepository,
    IGameTeamPlayerRepository gameTeamPlayerRepository)
    : IRequestHandler<CreateGameTeamPlayerCommand, CreateGameTeamPlayerViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamRepository _gameTeamRepository = gameTeamRepository;
    private readonly IGameTeamPlayerRepository _gameTeamPlayerRepository = gameTeamPlayerRepository;

    public async Task<CreateGameTeamPlayerViewModel> Handle(CreateGameTeamPlayerCommand request, CancellationToken cancellationToken)
    {
        GameTeam gameTeam = await _gameTeamRepository.GetByIdAsync(request.GameTeamPlayer.GameId, request.GameTeamPlayer.TeamId).ConfigureAwait(true)
            ?? throw new NotFoundException(Localization.GameTeamNotFound);

        GameTeamPlayer gameTeamPlayer = _mapper.Map<GameTeamPlayer>(request.GameTeamPlayer)
                                               .SetGameTeamId(gameTeam.Id)
                                               .SetStatus(TeamPlayerStatusEnum.Active);

        gameTeamPlayer.AddDomainEvent(new GameTeamPlayerCreatedEvent(gameTeamPlayer));

        await _gameTeamPlayerRepository.AddAsync(gameTeamPlayer)
                                       .ConfigureAwait(true);

        return _mapper.Map<CreateGameTeamPlayerViewModel>(gameTeamPlayer);
    }
}