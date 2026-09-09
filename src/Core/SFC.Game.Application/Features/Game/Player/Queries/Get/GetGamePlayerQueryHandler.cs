using AutoMapper;

using MediatR;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Player;
using SFC.Game.Domain.Entities.Game.Player;

namespace SFC.Game.Application.Features.Game.Player.Queries.Get;
public class GetGamePlayerQueryHandler(IMapper mapper, IGamePlayerRepository gamePlayerRepository)
    : IRequestHandler<GetGamePlayerQuery, GetGamePlayerViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGamePlayerRepository _gamePlayerRepository = gamePlayerRepository;

    public async Task<GetGamePlayerViewModel> Handle(GetGamePlayerQuery request, CancellationToken cancellationToken)
    {
        GamePlayer gamePlayer = await _gamePlayerRepository.GetByIdAsync(request.GameId, request.PlayerId, request.Includes).ConfigureAwait(true)
            ?? throw new NotFoundException(Localization.GamePlayerNotFound);

        return _mapper.Map<GetGamePlayerViewModel>(gamePlayer);
    }
}