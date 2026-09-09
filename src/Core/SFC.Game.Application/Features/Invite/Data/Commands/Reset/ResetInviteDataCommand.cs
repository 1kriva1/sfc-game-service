using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Invite.Data.Common.Dto;

namespace SFC.Game.Application.Features.Invite.Data.Commands.Reset;
public class ResetInviteDataCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.ResetInviteData; }

    public IEnumerable<InviteStatusDto> InviteStatuses { get; init; } = [];
}