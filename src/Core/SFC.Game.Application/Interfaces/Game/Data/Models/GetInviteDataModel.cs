using SFC.Game.Domain.Entities.Game.Data;

namespace SFC.Game.Application.Interfaces.Game.Data.Models;
public record GetInviteDataModel
{
    public IEnumerable<GameStatus> GameStatuses { get; init; } = [];

    public IEnumerable<GamePlayerStatus> GamePlayerStatuses { get; init; } = [];

    public IEnumerable<GameTeamStatus> GameTeamStatuses { get; init; } = [];

    public IEnumerable<GameTeamIndex> GameTeamIndexes { get; init; } = [];
}