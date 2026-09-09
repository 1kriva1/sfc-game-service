using SFC.Game.Domain.Entities.Request.Data;

namespace SFC.Game.Application.Interfaces.Persistence.Context;
public interface IRequestDbContext : IDbContext
{
    IQueryable<RequestStatus> RequestStatuses { get; }
}