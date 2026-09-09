using AutoMapper;

using MediatR;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;

namespace SFC.Game.Application.Features.Game.General.Queries.Get;
public class GetGameQueryHandler(IMapper mapper, IGameRepository gameRepository)
    : IRequestHandler<GetGameQuery, GetGameViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameRepository _gameRepository = gameRepository;

    public async Task<GetGameViewModel> Handle(GetGameQuery request, CancellationToken cancellationToken)
    {
        GameEntity game = await _gameRepository.GetByIdAsync(request.Id, request.Includes).ConfigureAwait(true)
            ?? throw new NotFoundException(Localization.GameNotFound);

        return _mapper.Map<GetGameViewModel>(game);
    }
}