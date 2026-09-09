using SFC.Game.Domain.Common;
using SFC.Game.Domain.Entities.Identity;

namespace SFC.Game.Domain.Events.Identity;
public class UsersCreatedEvent(IEnumerable<User> users) : BaseEvent
{
    public IEnumerable<User> Users { get; } = users;
}