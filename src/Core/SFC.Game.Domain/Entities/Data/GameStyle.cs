using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Data;
public class GameStyle : EnumDataEntity<GameStyleEnum>
{
    public GameStyle() : base() { }

    public GameStyle(GameStyleEnum enumType) : base(enumType) { }
}