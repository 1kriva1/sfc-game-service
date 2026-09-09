using Microsoft.Extensions.DependencyInjection;

using SFC.Game.Application.Interfaces.Cache;
using SFC.Game.Application.Interfaces.Persistence.Repository.Data;
using SFC.Game.Domain.Entities.Data;
using SFC.Game.Infrastructure.Persistence.Constants;
using SFC.Scheme.Infrastructure.Persistence.Repositories.Data.Cache;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Data.Cache;

public class ShirtCacheRepository(ShirtRepository repository, [FromKeyedServices(CacheInstance.Data)] ICache cache)
    : DataCacheRepository<Shirt, ShirtEnum>(repository, cache), IShirtRepository
{ }