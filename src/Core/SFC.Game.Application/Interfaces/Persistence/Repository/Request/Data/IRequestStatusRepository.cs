using SFC.Game.Domain.Entities.Request.Data;

namespace SFC.Game.Application.Interfaces.Persistence.Repository.Request.Data;
public interface IRequestStatusRepository : IRequestDataRepository<RequestStatus, RequestStatusEnum> { }