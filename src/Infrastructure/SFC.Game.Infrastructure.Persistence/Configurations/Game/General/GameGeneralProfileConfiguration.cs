using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Domain.Entities.Game.General;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Game.General;

public class GameGeneralProfileConfiguration : IEntityTypeConfiguration<GameGeneralProfile>
{
    public void Configure(EntityTypeBuilder<GameGeneralProfile> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Property(e => e.Name)
            .HasMaxLength(ValidationConstants.NameValueMaxLength)
            .IsRequired(true);

        builder.Property(e => e.Description)
            .HasMaxLength(ValidationConstants.DescriptionValueMaxLength)
            .IsRequired(false);

        builder.ToTable("GeneralProfiles");
    }
}