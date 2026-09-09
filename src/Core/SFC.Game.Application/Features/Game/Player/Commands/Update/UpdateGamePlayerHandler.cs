using AutoMapper;

using MediatR;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Player;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Events.Game.Player;

namespace SFC.Game.Application.Features.Game.Player.Commands.Update;
public class UpdateGamePlayerHandler(IMapper mapper, IGamePlayerRepository gamePlayerRepository)
    : IRequestHandler<UpdateGamePlayerCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGamePlayerRepository _gamePlayerRepository = gamePlayerRepository;

    public async Task Handle(UpdateGamePlayerCommand request, CancellationToken cancellationToken)
    {
        GamePlayer gamePlayer = await _gamePlayerRepository
            .GetByIdAsync(request.GamePlayer.GameId, request.GamePlayer.PlayerId).ConfigureAwait(true)
                ?? throw new NotFoundException(Localization.GamePlayerNotFound);

        GamePlayer updatedGamePlayer = _mapper.Map(request.GamePlayer, gamePlayer);

        updatedGamePlayer.AddDomainEvent(new GamePlayerUpdatedEvent(updatedGamePlayer));

        await _gamePlayerRepository.UpdateAsync(updatedGamePlayer)
                                 .ConfigureAwait(false);
    }
}