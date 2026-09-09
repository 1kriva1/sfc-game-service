using Microsoft.Extensions.DependencyInjection;

using SFC.Game.Application.Interfaces.Cache;
using SFC.Game.Application.Interfaces.Persistence.Repository.Invite.Data;
using SFC.Game.Domain.Entities.Invite.Data;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Invite.Data.Cache;
public class InviteStatusCacheRepository(InviteStatusRepository repository, [FromKeyedServices(CacheInstance.Invite)] ICache cache)
    : InviteDataCacheRepository<InviteStatus, InviteStatusEnum>(repository, cache), IInviteStatusRepository
{ }