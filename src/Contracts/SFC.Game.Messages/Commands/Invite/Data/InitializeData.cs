using SFC.Game.Messages.Models.Data;

namespace SFC.Game.Messages.Commands.Invite.Data;
public record InitializeData
{
    public IEnumerable<DataValue> InviteStatuses { get; init; } = [];
}