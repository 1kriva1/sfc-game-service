using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Game.Data;
public class GamePlayerStatus : EnumDataEntity<GamePlayerStatusEnum>
{
    public GamePlayerStatus() : base() { }

    public GamePlayerStatus(GamePlayerStatusEnum enumType) : base(enumType) { }
}