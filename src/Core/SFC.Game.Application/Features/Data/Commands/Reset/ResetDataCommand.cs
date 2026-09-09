using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Data.Common.Dto;

namespace SFC.Game.Application.Features.Data.Commands.Reset;
public class ResetDataCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.ResetData; }

    public IEnumerable<FootballPositionDto> FootballPositions { get; init; } = [];

    public IEnumerable<GameStyleDto> GameStyles { get; init; } = [];

    public IEnumerable<StatCategoryDto> StatCategories { get; init; } = [];

    public IEnumerable<StatSkillDto> StatSkills { get; init; } = [];

    public IEnumerable<StatTypeDto> StatTypes { get; init; } = [];

    public IEnumerable<WorkingFootDto> WorkingFoots { get; init; } = [];

    public IEnumerable<ShirtDto> Shirts { get; init; } = [];
}