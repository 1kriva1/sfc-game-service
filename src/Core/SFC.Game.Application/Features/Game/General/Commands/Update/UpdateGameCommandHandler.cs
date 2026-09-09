using AutoMapper;

using MediatR;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;
using SFC.Game.Domain.Events.Game;
using SFC.Game.Domain.Events.Game.General;

namespace SFC.Game.Application.Features.Game.General.Commands.Update;
public class UpdateGameCommandHandler(IMapper mapper, IGameRepository gameRepository)
    : IRequestHandler<UpdateGameCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameRepository _gameRepository = gameRepository;

    public async Task Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        GameEntity game = await _gameRepository.GetByIdAsync(request.GameId).ConfigureAwait(true)
            ?? throw new NotFoundException(Localization.GameNotFound);

        GameEntity updatedGame = _mapper.Map(request.Game, game);

        updatedGame.AddDomainEvent(new GameUpdatedEvent(updatedGame));

        await _gameRepository.UpdateAsync(updatedGame)
                             .ConfigureAwait(false);
    }
}