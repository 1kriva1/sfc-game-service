using Microsoft.EntityFrameworkCore;

using SFC.Game.Application.Interfaces.Persistence.Repository.Data;
using SFC.Game.Domain.Entities.Data;
using SFC.Game.Infrastructure.Persistence.Contexts;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Data;

public class StatTypeRepository(DataDbContext context)
    : DataRepository<StatType, StatTypeEnum>(context), IStatTypeRepository
{
    public virtual Task<int> CountAsync() => Context.StatTypes.CountAsync();

    public override async Task<IReadOnlyList<StatType>> ListAllAsync()
    {
        return await Context.StatTypes
                            .AsNoTracking()
                            .ToListAsync()
                            .ConfigureAwait(false);
    }
}