using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Data;
public class FootballPosition : EnumDataEntity<FootballPositionEnum>
{
    public FootballPosition() : base() { }

    public FootballPosition(FootballPositionEnum enumType) : base(enumType) { }
}