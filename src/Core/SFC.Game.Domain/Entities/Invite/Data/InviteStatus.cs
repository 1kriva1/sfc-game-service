using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Invite.Data;
public class InviteStatus : EnumDataEntity<InviteStatusEnum>
{
    public InviteStatus() : base() { }

    public InviteStatus(InviteStatusEnum enumType) : base(enumType) { }
}