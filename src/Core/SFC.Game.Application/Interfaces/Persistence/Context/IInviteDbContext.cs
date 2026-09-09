using SFC.Game.Domain.Entities.Invite.Data;

namespace SFC.Game.Application.Interfaces.Persistence.Context;
public interface IInviteDbContext : IDbContext
{
    IQueryable<InviteStatus> InviteStatuses { get; }
}