using SFC.Game.Messages.Models.Data;

namespace SFC.Game.Messages.Events.Game.Data;
public record DataInitialized
{
    public IEnumerable<DataValue> GameStatuses { get; init; } = [];

    public IEnumerable<DataValue> GameTeamStatuses { get; init; } = [];

    public IEnumerable<DataValue> GamePlayerStatuses { get; init; } = [];

    public IEnumerable<DataValue> GameTeamIndexes { get; init; } = [];
}