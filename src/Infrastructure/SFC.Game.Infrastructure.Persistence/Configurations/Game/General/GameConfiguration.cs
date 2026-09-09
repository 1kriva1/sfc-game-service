using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Game.Data;
using SFC.Game.Domain.Entities.Game.General;
using SFC.Game.Domain.Entities.Identity;
using SFC.Game.Domain.Entities.Team.General;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Game.General;
public class GameConfiguration : AuditableEntityConfiguration<GameEntity, long>
{
    public override void Configure(EntityTypeBuilder<GameEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasOne<User>()
               .WithMany()
               .IsRequired(true);

        builder.HasOne<GameStatus>()
               .WithMany()
               .HasForeignKey(t => t.StatusId)
               .IsRequired(true);

        builder.HasOne(e => e.GeneralProfile)
               .WithOne(e => e.Game)
               .HasForeignKey<GameGeneralProfile>()
               .IsRequired(true);

        builder.HasOne(e => e.FinancialProfile)
               .WithOne(e => e.Game)
               .HasForeignKey<GameFinancialProfile>()
               .IsRequired(true);

        builder.HasOne(e => e.InventaryProfile)
               .WithOne(e => e.Game)
               .HasForeignKey<GameInventaryProfile>()
               .IsRequired(true);

        builder.HasOne(e => e.Availability)
               .WithOne(e => e.Game)
               .HasForeignKey<GameAvailability>()
               .IsRequired(true);

        builder.HasMany(e => e.Tags)
               .WithOne(e => e.Game)
               .HasForeignKey(DatabaseConstants.GameForeignKey);

        builder.HasMany(e => e.Players)
               .WithOne()
               .HasForeignKey(e => e.GameId)
               .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany(e => e.Teams)
               .WithOne()
               .HasForeignKey(e => e.GameId)
               .OnDelete(DeleteBehavior.ClientCascade);

        builder.ToTable("Games");

        base.Configure(builder);
    }
}