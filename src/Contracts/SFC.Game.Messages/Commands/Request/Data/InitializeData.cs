using SFC.Game.Messages.Models.Data;

namespace SFC.Game.Messages.Commands.Request.Data;
public record InitializeData
{
    public IEnumerable<DataValue> RequestStatuses { get; init; } = [];
}