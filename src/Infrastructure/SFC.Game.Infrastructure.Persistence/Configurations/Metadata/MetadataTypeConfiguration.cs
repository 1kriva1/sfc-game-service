using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Metadata;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Metadata;
public class MetadataTypeConfiguration : EnumEntityConfiguration<MetadataType, MetadataTypeEnum>
{
    public override void Configure(EntityTypeBuilder<MetadataType> builder)
    {
        builder.ToTable("Types", DatabaseConstants.MetadataSchemaName);
        base.Configure(builder);
    }
}