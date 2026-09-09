using AutoMapper;

using MediatR;

using SFC.Game.Application.Features.Game.General.Common.Extensions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;
using SFC.Game.Domain.Events.Game.General;

namespace SFC.Game.Application.Features.Game.General.Commands.Create;
public class CreateGameCommandHandler(
    IMapper mapper,
    IGameRepository gameRepository)
    : IRequestHandler<CreateGameCommand, CreateGameViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameRepository _gameRepository = gameRepository;

    public async Task<CreateGameViewModel> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        GameEntity game = _mapper.Map<GameEntity>(request.Game)
                                 .SetStatus(GameStatusEnum.New);

        game.AddDomainEvent(new GameCreatedEvent(game));

        await _gameRepository.AddAsync(game)
                             .ConfigureAwait(false);

        return _mapper.Map<CreateGameViewModel>(game);
    }
}