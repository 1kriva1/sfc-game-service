using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Metadata;
public class MetadataDomain : EnumEntity<MetadataDomainEnum>
{
    public MetadataDomain() : base() { }

    public MetadataDomain(MetadataDomainEnum enumType) : base(enumType) { }
}