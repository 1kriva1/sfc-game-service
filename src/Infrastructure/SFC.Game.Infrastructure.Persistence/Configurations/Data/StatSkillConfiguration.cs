using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Data;
public class StatSkillConfiguration : EnumDataEntityConfiguration<StatSkill, StatSkillEnum>
{
    public override void Configure(EntityTypeBuilder<StatSkill> builder)
    {
        builder.HasMany(e => e.Types)
               .WithOne()
               .HasForeignKey(t => t.SkillId)
               .IsRequired(true);

        builder.ToTable("StatSkills", DatabaseConstants.DataSchemaName);

        base.Configure(builder);
    }
}