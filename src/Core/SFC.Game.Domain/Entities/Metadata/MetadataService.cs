using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Metadata;
public class MetadataService : EnumEntity<MetadataServiceEnum>
{
    public MetadataService() : base() { }

    public MetadataService(MetadataServiceEnum enumType) : base(enumType) { }
}