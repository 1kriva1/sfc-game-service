using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Domain.Entities.Player;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Player;
public class PlayerPhotoConfiguration : IEntityTypeConfiguration<PlayerPhoto>
{
    public void Configure(EntityTypeBuilder<PlayerPhoto> builder)
    {
        builder.Property(e => e.Source)
               .HasColumnType("image")
               .IsRequired(true);

        builder.Property(e => e.Extension)
               .HasConversion<string>()
               .HasMaxLength(ValidationConstants.ExtensionValueMaxLength)
               .IsRequired(true);

        builder.Property(e => e.Name)
               .HasMaxLength(ValidationConstants.NameValueMaxLength)
               .IsRequired(true);

        builder.Property(e => e.Size)
               .HasMaxLength(ValidationConstants.FileMaxSizeInBytes)
               .IsRequired(true);

        builder.ToTable("Photos", DatabaseConstants.PlayerSchemaName);
    }
}