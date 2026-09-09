using SFC.Game.Messages.Models.Data;

namespace SFC.Game.Messages.Commands.Team.Data;
public record InitializeData
{
    public IEnumerable<DataValue> TeamPlayerStatuses { get; init; } = [];
}