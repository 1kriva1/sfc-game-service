using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Team.Data;
public class TeamPlayerStatus : EnumDataEntity<TeamPlayerStatusEnum>
{
    public TeamPlayerStatus() : base() { }

    public TeamPlayerStatus(TeamPlayerStatusEnum enumType) : base(enumType) { }
}