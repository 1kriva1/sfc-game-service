using SFC.Game.Application.Features.Common.Dto.Common;

namespace SFC.Game.Application.Common.Dto.Player.General.Filters;
public class PlayerStatsBySkillRangeLimitFilterDto : RangeLimitDto<short?>
{
    public int Skill { get; set; }
}