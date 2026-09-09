using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using SFC.Game.Application.Common.Settings;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Cache;

public class RedisInviteCache([FromKeyedServices(CacheInstance.Invite)] IDistributedCache cache, IOptions<CacheSettings> cacheConfig)
    : RedisCache(cache, cacheConfig)
{ }