using SFC.Game.Domain.Entities.Game.Data;

namespace SFC.Game.Application.Interfaces.Persistence.Repository.Game.Data;
public interface IGameStatusRepository : IGameDataRepository<GameStatus, GameStatusEnum> { }