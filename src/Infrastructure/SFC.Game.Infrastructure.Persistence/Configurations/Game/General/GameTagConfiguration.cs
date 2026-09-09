using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Domain.Entities.Game.General;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Game.General;
public class GameTagConfiguration : IEntityTypeConfiguration<GameTag>
{
    public void Configure(EntityTypeBuilder<GameTag> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Property(e => e.Value)
            .HasMaxLength(ValidationConstants.TagValueMaxLength)
            .IsRequired(true);

        builder.ToTable("Tags");
    }
}