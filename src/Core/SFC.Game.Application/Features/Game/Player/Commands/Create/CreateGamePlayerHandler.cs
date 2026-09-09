using AutoMapper;

using MediatR;

using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Player;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Events.Game.Player;

namespace SFC.Game.Application.Features.Game.Player.Commands.Create;
public class CreateGamePlayerHandler(
    IMapper mapper,
    IGamePlayerRepository gamePlayerRepository)
    : IRequestHandler<CreateGamePlayerCommand, CreateGamePlayerViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGamePlayerRepository _gamePlayerRepository = gamePlayerRepository;

    public async Task<CreateGamePlayerViewModel> Handle(CreateGamePlayerCommand request, CancellationToken cancellationToken)
    {
        GamePlayer gamePlayer = _mapper.Map<GamePlayer>(request.GamePlayer);

        gamePlayer.AddDomainEvent(new GamePlayerCreatedEvent(gamePlayer));

        await _gamePlayerRepository.AddAsync(gamePlayer)
                                 .ConfigureAwait(true);

        return _mapper.Map<CreateGamePlayerViewModel>(gamePlayer);
    }
}