using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Metadata;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Metadata;
public class MetadataServiceConfiguration : EnumEntityConfiguration<MetadataService, MetadataServiceEnum>
{
    public override void Configure(EntityTypeBuilder<MetadataService> builder)
    {
        builder.ToTable("Services", DatabaseConstants.MetadataSchemaName);
        base.Configure(builder);
    }
}