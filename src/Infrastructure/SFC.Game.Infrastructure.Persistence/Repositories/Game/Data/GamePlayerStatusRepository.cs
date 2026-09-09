using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Data;
using SFC.Game.Domain.Entities.Game.Data;
using SFC.Game.Infrastructure.Persistence.Contexts;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Game.Data;
public class GamePlayerStatusRepository(GameDbContext context)
    : GameDataRepository<GamePlayerStatus, GamePlayerStatusEnum>(context), IGamePlayerStatusRepository
{ }