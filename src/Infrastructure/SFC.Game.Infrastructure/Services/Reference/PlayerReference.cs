using AutoMapper;

using SFC.Game.Application.Common.Dto.Player.General;
using SFC.Game.Application.Interfaces.Persistence.Repository.Player;
using SFC.Game.Application.Interfaces.Player;
using SFC.Game.Application.Interfaces.Reference;

namespace SFC.Game.Infrastructure.Services.Reference;
public class PlayerReference(
    IMapper mapper,
    IPlayerRepository playerRepository,
    IPlayerService playerService) : BaseReference<PlayerEntity, long, PlayerDto>(mapper), IPlayerReference
{
    private readonly IPlayerRepository _playerRepository = playerRepository;
    private readonly IPlayerService _playerService = playerService;

    protected override Task<PlayerEntity?> GetFromLocalAsync(long id, CancellationToken cancellationToken = default)
        => _playerRepository.GetByIdAsync(id);

    protected override Task<PlayerDto?> GetFromReferenceAsync(long id, CancellationToken cancellationToken = default)
        => _playerService.GetPlayerAsync(id, cancellationToken);

    protected override Task<PlayerEntity> AddLocalAsync(PlayerEntity entity, CancellationToken cancellationToken = default)
        => _playerRepository.AddAsync(entity);
}