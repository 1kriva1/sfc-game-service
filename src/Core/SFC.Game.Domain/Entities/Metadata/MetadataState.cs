using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Metadata;
public class MetadataState : EnumEntity<MetadataStateEnum>
{
    public MetadataState() : base() { }

    public MetadataState(MetadataStateEnum enumType) : base(enumType) { }
}