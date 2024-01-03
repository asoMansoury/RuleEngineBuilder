using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RuleBuilderInfra.Domain.Entities
{
    public class ActionRuleEntityConfiguration : IEntityTypeConfiguration<ActionRuleEntity>
    {
        public void Configure(EntityTypeBuilder<ActionRuleEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne((parent) => parent.RuleEntity)
                .WithMany((parent) => parent.actionRuleEntities)
                .HasForeignKey(foreign => foreign.RuleEntityID);

            builder.HasOne((parent) => parent.ActionEntity)
                        .WithMany((parent) => parent.actionRuleEntities)
                        .HasForeignKey(foreign => foreign.ActionEntityID);

            builder.HasMany((condtion) => condtion.actionRulePropertiesEntities)
                .WithOne((ruleItem) => ruleItem.ActionRuleEntity)
                .HasForeignKey((foreignKey) => foreignKey.ActionRuleEntityId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
