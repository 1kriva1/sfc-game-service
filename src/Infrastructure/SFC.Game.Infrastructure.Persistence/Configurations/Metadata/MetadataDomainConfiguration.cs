using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Metadata;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Metadata;
public class MetadataDomainConfiguration : EnumEntityConfiguration<MetadataDomain, MetadataDomainEnum>
{
    public override void Configure(EntityTypeBuilder<MetadataDomain> builder)
    {
        builder.ToTable("Domains", DatabaseConstants.MetadataSchemaName);
        base.Configure(builder);
    }
}