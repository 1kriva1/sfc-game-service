using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Domain.Entities.Player;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Player;
public class PlayerTagConfiguration : IEntityTypeConfiguration<PlayerTag>
{
    public void Configure(EntityTypeBuilder<PlayerTag> builder)
    {
        builder.Property(e => e.Value)
               .HasMaxLength(ValidationConstants.TagValueMaxLength)
               .IsRequired(true);

        builder.ToTable("Tags", DatabaseConstants.PlayerSchemaName);
    }
}