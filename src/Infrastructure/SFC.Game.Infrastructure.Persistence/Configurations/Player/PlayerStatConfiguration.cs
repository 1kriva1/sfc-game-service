using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Data;
using SFC.Game.Domain.Entities.Player;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Player;
public class PlayerStatConfiguration : IEntityTypeConfiguration<PlayerStat>
{
    public void Configure(EntityTypeBuilder<PlayerStat> builder)
    {
        builder.HasOne<StatType>(e => e.Type)
               .WithMany()
               .HasForeignKey(t => t.TypeId)
               .IsRequired(true);

        builder.ToTable("Stats", DatabaseConstants.PlayerSchemaName);
    }
}