using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Player;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Player;
public class PlayerAvailableDayConfiguration : IEntityTypeConfiguration<PlayerAvailableDay>
{
    public void Configure(EntityTypeBuilder<PlayerAvailableDay> builder)
    {
        builder.Property(e => e.Day)
            .HasConversion<byte>()
            .IsRequired(true);

        builder.ToTable("AvailableDays", DatabaseConstants.PlayerSchemaName);
    }
}