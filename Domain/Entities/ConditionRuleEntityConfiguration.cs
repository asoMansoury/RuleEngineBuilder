using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RuleBuilderInfra.Domain.Entities
{
    public class ConditionRuleEntityConfiguration : IEntityTypeConfiguration<ConditionRuleEntity>
    {
        public void Configure(EntityTypeBuilder<ConditionRuleEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.HasOne((c => c.Parent))
                .WithMany(c => c.Conditions)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne((ruleEntity) => ruleEntity.RuleEntity)
                    .WithMany((ruleEntities) => ruleEntities.ConditionRulesEntity)
                    .HasForeignKey((ruleEntityItem) => ruleEntityItem.RuleEntityId);

            builder.HasOne((conditionEntity) => conditionEntity.ConditionEntity)
                        .WithMany((item) => item.ConditionRuleEntities)
                        .HasForeignKey((item) => item.ConditionEntityId);

        }
    }
}
