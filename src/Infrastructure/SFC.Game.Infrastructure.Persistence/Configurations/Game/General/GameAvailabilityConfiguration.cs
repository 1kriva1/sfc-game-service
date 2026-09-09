using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Game.General;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Game.General;
public class GameAvailabilityConfiguration : IEntityTypeConfiguration<GameAvailability>
{
    public void Configure(EntityTypeBuilder<GameAvailability> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Property(e => e.Date)
               .IsRequired(true);

        builder.Property(e => e.From)
               .IsRequired(true);

        builder.Property(e => e.To)
               .IsRequired(true);

        builder.ToTable("Availabilities");
    }
}