using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Game.General;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Game.General;
public class GameFinancialProfileConfiguration : IEntityTypeConfiguration<GameFinancialProfile>
{
    public void Configure(EntityTypeBuilder<GameFinancialProfile> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Property(e => e.FreeGame)
            .HasDefaultValue(false);

        builder.Property(e => e.PayAmount)
            .HasPrecision(18, 2);

        builder.ToTable("FinancialProfiles");
    }
}