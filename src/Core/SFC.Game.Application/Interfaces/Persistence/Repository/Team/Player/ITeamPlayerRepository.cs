using SFC.Game.Application.Interfaces.Persistence.Context;
using SFC.Game.Application.Interfaces.Persistence.Repository.Common;
using SFC.Game.Domain.Entities.Team.Player;

namespace SFC.Game.Application.Interfaces.Persistence.Repository.Team.Player;
public interface ITeamPlayerRepository : IRepository<TeamPlayer, ITeamDbContext, long>
{
    Task<bool> AnyAsync(long teamId, long playerId);

    Task<bool> AnyAsync(long teamId, long playerId, TeamPlayerStatusEnum status);

    Task<TeamPlayer?> GetByIdAsync(long teamId, long playerId);

    Task<TeamPlayer[]> AddRangeIfNotExistsAsync(params TeamPlayer[] entities);
}