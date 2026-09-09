using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Game.Data;
public class GameTeamIndex : EnumDataEntity<GameTeamIndexEnum>
{
    public GameTeamIndex() : base() { }

    public GameTeamIndex(GameTeamIndexEnum enumType) : base(enumType) { }
}