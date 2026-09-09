using SFC.Game.Application.Features.Game.Data.Queries.Common.Dto;

namespace SFC.Game.Application.Features.Game.Data.Queries.GetAll;

public record GetAllGameDataViewModel
{
    public IEnumerable<DataValueDto> GameStatuses { get; init; } = [];

    public IEnumerable<DataValueDto> GameTeamStatuses { get; init; } = [];

    public IEnumerable<DataValueDto> GamePlayerStatuses { get; init; } = [];

    public IEnumerable<DataValueDto> GameTeamIndexes { get; init; } = [];
}