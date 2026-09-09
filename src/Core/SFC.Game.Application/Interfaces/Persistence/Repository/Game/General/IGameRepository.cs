using SFC.Game.Application.Features.Common.Models.Find;
using SFC.Game.Application.Features.Common.Models.Find.Paging;
using SFC.Game.Application.Interfaces.Persistence.Context;
using SFC.Game.Application.Interfaces.Persistence.Repository.Common;

namespace SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;

/// <summary>
/// Repository for core entity of the service.
/// </summary>
public interface IGameRepository : IRepository<GameEntity, IGameDbContext, long>
{
    Task<bool> AnyAsync(long id);

    Task<bool> AnyAsync(long id, Guid userId);

    Task<IEnumerable<GameEntity>> GetByUserIdsAsync(IEnumerable<Guid> userIds);

    Task<GameEntity?> GetByIdAsync(long id, IEnumerable<string>? includes);

    Task<PagedList<GameEntity>> FindAsync(FindParameters<GameEntity> parameters, IEnumerable<string>? includes);

    Task<GameEntity[]> AddRangeIfNotExistsAsync(params GameEntity[] entities);
}