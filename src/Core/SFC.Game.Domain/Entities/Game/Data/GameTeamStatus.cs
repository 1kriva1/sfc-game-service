using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Game.Data;
public class GameTeamStatus : EnumDataEntity<GameTeamStatusEnum>
{
    public GameTeamStatus() : base() { }

    public GameTeamStatus(GameTeamStatusEnum enumType) : base(enumType) { }
}