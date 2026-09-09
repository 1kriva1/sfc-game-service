using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Data;
public class ShirtConfiguration : EnumDataEntityConfiguration<Shirt, ShirtEnum>
{
    public override void Configure(EntityTypeBuilder<Shirt> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("Shirts", DatabaseConstants.DataSchemaName);
        base.Configure(builder);
    }
}