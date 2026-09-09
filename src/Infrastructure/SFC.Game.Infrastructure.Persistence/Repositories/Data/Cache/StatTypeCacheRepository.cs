using Microsoft.Extensions.DependencyInjection;

using SFC.Game.Application.Interfaces.Cache;
using SFC.Game.Application.Interfaces.Persistence.Repository.Data;
using SFC.Game.Domain.Entities.Data;
using SFC.Game.Infrastructure.Persistence.Constants;
using SFC.Scheme.Infrastructure.Persistence.Repositories.Data.Cache;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Data.Cache;

public class StatTypeCacheRepository(StatTypeRepository repository, [FromKeyedServices(CacheInstance.Data)] ICache cache)
    : DataCacheRepository<StatType, StatTypeEnum>(repository, cache), IStatTypeRepository
{
#pragma warning disable CA1859 // Use concrete types when possible for improved performance
    private readonly IStatTypeRepository _repository = repository;
#pragma warning restore CA1859 // Use concrete types when possible for improved performance

    public Task<int> CountAsync()
    {
        return !Cache.TryGet(CacheKey, out IReadOnlyList<StatType> entities)
            ? _repository.CountAsync()
            : Task.FromResult(entities.Count);
    }
}