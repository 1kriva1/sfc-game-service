using AutoMapper;

using MediatR;

using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Events.Game.Team.General;

namespace SFC.Game.Application.Features.Game.Team.General.Commands.Create;
public class CreateGameTeamHandler(
    IMapper mapper,
    IGameTeamRepository gameTeamRepository)
    : IRequestHandler<CreateGameTeamCommand, CreateGameTeamViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamRepository _gameTeamRepository = gameTeamRepository;

    public async Task<CreateGameTeamViewModel> Handle(CreateGameTeamCommand request, CancellationToken cancellationToken)
    {
        GameTeam gameTeam = _mapper.Map<GameTeam>(request.GameTeam);

        gameTeam.AddDomainEvent(new GameTeamCreatedEvent(gameTeam));

        await _gameTeamRepository.AddAsync(gameTeam)
                                 .ConfigureAwait(true);

        return _mapper.Map<CreateGameTeamViewModel>(gameTeam);
    }
}