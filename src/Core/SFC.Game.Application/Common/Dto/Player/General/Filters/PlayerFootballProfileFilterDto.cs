using SFC.Game.Application.Features.Common.Dto.Common;

namespace SFC.Game.Application.Common.Dto.Player.General.Filters;
public class PlayerFootballProfileFilterDto
{
    public RangeLimitDto<short?> Height { get; set; } = default!;

    public RangeLimitDto<short?> Weight { get; set; } = default!;

    public IEnumerable<int> Positions { get; set; } = [];

    public int? WorkingFoot { get; set; }

    public IEnumerable<int> GameStyles { get; set; } = [];

    public int? Skill { get; set; }

    public int? PhysicalCondition { get; set; }
}