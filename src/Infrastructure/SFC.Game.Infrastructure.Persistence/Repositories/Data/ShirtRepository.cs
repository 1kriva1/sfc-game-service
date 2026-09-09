using SFC.Game.Application.Interfaces.Persistence.Repository.Data;
using SFC.Game.Domain.Entities.Data;
using SFC.Game.Infrastructure.Persistence.Contexts;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Data;
public class ShirtRepository(DataDbContext context)
    : DataRepository<Shirt, ShirtEnum>(context), IShirtRepository
{ }