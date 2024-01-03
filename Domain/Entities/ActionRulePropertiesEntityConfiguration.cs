using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace RuleBuilderInfra.Domain.Entities
{
    public class ActionRulePropertiesEntityConfiguration : IEntityTypeConfiguration<ActionRulePropertiesEntity>
    {
        public void Configure(EntityTypeBuilder<ActionRulePropertiesEntity> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.HasOne((parent) => parent.RuleEntity)
                .WithMany((parent) => parent.actionRulePropertiesEntities)
                .HasForeignKey(foreign => foreign.RuleEntityId);


            builder.HasOne((parent) => parent.ActionPropertyEntity)
                .WithMany((parent) => parent.actionRulePropertiesEntities)
                .HasForeignKey(foreign => foreign.ActionPropertyEntityId);

        }
    }
}
