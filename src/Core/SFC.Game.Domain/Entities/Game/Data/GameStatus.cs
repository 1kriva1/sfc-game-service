using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Game.Data;
public class GameStatus : EnumDataEntity<GameStatusEnum>
{
    public GameStatus() : base() { }

    public GameStatus(GameStatusEnum enumType) : base(enumType) { }
}