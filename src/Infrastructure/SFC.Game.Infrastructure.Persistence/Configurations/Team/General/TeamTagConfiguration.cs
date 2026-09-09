using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Domain.Entities.Team.General;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Team.General;
public class TeamTagConfiguration : IEntityTypeConfiguration<TeamTag>
{
    public void Configure(EntityTypeBuilder<TeamTag> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Property(e => e.Value)
            .HasMaxLength(ValidationConstants.TagValueMaxLength)
            .IsRequired(true);

        builder.ToTable("Tags", DatabaseConstants.TeamSchemaName);
    }
}