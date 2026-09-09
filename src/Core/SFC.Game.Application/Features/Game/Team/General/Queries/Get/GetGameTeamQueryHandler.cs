using AutoMapper;

using MediatR;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Domain.Entities.Game.Team.General;

namespace SFC.Game.Application.Features.Game.Team.General.Queries.Get;
public class GetGameTeamQueryHandler(IMapper mapper, IGameTeamRepository gameTeamRepository)
    : IRequestHandler<GetGameTeamQuery, GetGameTeamViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamRepository _gameTeamRepository = gameTeamRepository;

    public async Task<GetGameTeamViewModel> Handle(GetGameTeamQuery request, CancellationToken cancellationToken)
    {
        GameTeam gameTeam = await _gameTeamRepository.GetByIdAsync(request.GameId, request.TeamId, request.Includes).ConfigureAwait(true)
            ?? throw new NotFoundException(Localization.GameTeamNotFound);

        return _mapper.Map<GetGameTeamViewModel>(gameTeam);
    }
}