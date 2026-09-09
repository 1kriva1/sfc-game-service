using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Game.General;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Game.General;
public class GameInventaryProfileConfiguration : IEntityTypeConfiguration<GameInventaryProfile>
{
    public void Configure(EntityTypeBuilder<GameInventaryProfile> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Property(e => e.ShirtsRequired)
            .HasDefaultValue(false);

        builder.ToTable("InventaryProfiles");
    }
}