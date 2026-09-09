using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Data;
public class Shirt : EnumDataEntity<ShirtEnum>
{
    public Shirt() : base() { }

    public Shirt(ShirtEnum enumType) : base(enumType) { }
}