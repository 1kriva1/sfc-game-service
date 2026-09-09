using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Team.General;

namespace SFC.Game.Application.Common.Dto.Team.General;
public class TeamFinancialProfileDto : IMapFromReverse<TeamFinancialProfile>
{
    public bool FreePlay { get; set; }
}