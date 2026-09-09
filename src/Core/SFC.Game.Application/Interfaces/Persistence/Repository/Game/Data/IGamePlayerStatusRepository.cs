using SFC.Game.Domain.Entities.Game.Data;

namespace SFC.Game.Application.Interfaces.Persistence.Repository.Game.Data;
public interface IGamePlayerStatusRepository : IGameDataRepository<GamePlayerStatus, GamePlayerStatusEnum> { }