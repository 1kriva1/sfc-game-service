using AutoMapper;

using MediatR;

using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Domain.Entities.Game.Team.General;

namespace SFC.Game.Application.Features.Game.Team.General.Queries.Gets;
public class GetsGameTeamQueryHandler(IMapper mapper, IGameTeamRepository gameTeamRepository)
    : IRequestHandler<GetsGameTeamQuery, GetsGameTeamViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamRepository _gameTeamRepository = gameTeamRepository;

    public async Task<GetsGameTeamViewModel> Handle(GetsGameTeamQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<GameTeam> gameTeams = await _gameTeamRepository
            .ListAllAsync(request.GameId, request.Includes).ConfigureAwait(true);

        return _mapper.Map<GetsGameTeamViewModel>(gameTeams);
    }
}