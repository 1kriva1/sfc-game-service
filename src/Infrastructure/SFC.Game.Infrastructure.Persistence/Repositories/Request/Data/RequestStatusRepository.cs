using SFC.Game.Application.Interfaces.Persistence.Repository.Request.Data;
using SFC.Game.Domain.Entities.Request.Data;
using SFC.Game.Infrastructure.Persistence.Contexts;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Request.Data;
public class RequestStatusRepository(RequestDbContext context)
    : RequestDataRepository<RequestStatus, RequestStatusEnum>(context), IRequestStatusRepository
{ }