using AutoMapper;

using MediatR;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Domain.Entities.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Queries.Get;

public class GetGameTeamPlayerQueryHandler(IMapper mapper, IGameTeamPlayerRepository gameTeamPlayerRepository)
    : IRequestHandler<GetGameTeamPlayerQuery, GetGameTeamPlayerViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamPlayerRepository _gameTeamPlayerRepository = gameTeamPlayerRepository;

    public async Task<GetGameTeamPlayerViewModel> Handle(GetGameTeamPlayerQuery request, CancellationToken cancellationToken)
    {
        GameTeamPlayer team = await _gameTeamPlayerRepository.GetByIdAsync(request.GameId, request.TeamId, request.PlayerId, request.Includes).ConfigureAwait(true)
            ?? throw new NotFoundException(Localization.GameTeamPlayerNotFound);

        return _mapper.Map<GetGameTeamPlayerViewModel>(team);
    }
}