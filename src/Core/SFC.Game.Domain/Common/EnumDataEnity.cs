using SFC.Game.Domain.Common.Interfaces;

namespace SFC.Game.Domain.Common;

/// <summary>
/// Main parent class for Enum + Data entity (from Data service).
/// </summary>
/// <typeparam name="TEnum">Enum type as a base for entity.</typeparam>
public class EnumDataEntity<TEnum> : EnumEntity<TEnum>, IDataEntity
    where TEnum : struct
{
    public EnumDataEntity() : base() { }

    public EnumDataEntity(TEnum enumType) : base(enumType) { }

    public DateTime CreatedDate { get; set; }
}