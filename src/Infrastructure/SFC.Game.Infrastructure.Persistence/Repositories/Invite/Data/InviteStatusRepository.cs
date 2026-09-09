using SFC.Game.Application.Interfaces.Persistence.Repository.Invite.Data;
using SFC.Game.Domain.Entities.Invite.Data;
using SFC.Game.Infrastructure.Persistence.Contexts;

namespace SFC.Game.Infrastructure.Persistence.Repositories.Invite.Data;
public class InviteStatusRepository(InviteDbContext context)
    : InviteDataRepository<InviteStatus, InviteStatusEnum>(context), IInviteStatusRepository
{ }