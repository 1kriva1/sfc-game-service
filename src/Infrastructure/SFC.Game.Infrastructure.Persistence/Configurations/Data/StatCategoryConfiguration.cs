using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Data;
public class StatCategoryConfiguration : EnumDataEntityConfiguration<StatCategory, StatCategoryEnum>
{
    public override void Configure(EntityTypeBuilder<StatCategory> builder)
    {
        builder.HasMany(e => e.Types)
               .WithOne()
               .HasForeignKey(t => t.CategoryId)
               .IsRequired(true);

        builder.ToTable("StatCategories", DatabaseConstants.DataSchemaName);
        base.Configure(builder);
    }
}