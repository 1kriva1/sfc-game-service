using SFC.Game.Application.Interfaces.Persistence.Repository.Identity;
using SFC.Game.Domain.Entities.Identity;
using SFC.Game.Infrastructure.Persistence.Contexts;
using SFC.Game.Infrastructure.Persistence.Extensions;
using SFC.Game.Infrastructure.Persistence.Repositories.Common;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Identity;
public class UserRepository(IdentityDbContext context)
    : Repository<User, IdentityDbContext, Guid>(context), IUserRepository
{
    public async Task<User[]> AddRangeIfNotExistsAsync(params User[] users)
    {
        await Context.Set<User>().AddRangeIfNotExistsAsync<User, Guid>(users).ConfigureAwait(false);

        await Context.SaveChangesAsync().ConfigureAwait(false);

        return users;
    }
}