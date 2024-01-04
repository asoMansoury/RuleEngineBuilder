using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RuleBuilderInfra.Domain.Entities
{
    public class ConditionEntityConfiguration : IEntityTypeConfiguration<ConditionEntity>
    {
        public void Configure(EntityTypeBuilder<ConditionEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasMany((condtionRuleEntity) => condtionRuleEntity.ConditionRuleEntities)
                    .WithOne((conditionEntity) => conditionEntity.ConditionEntity)
                    .HasForeignKey((fk) => fk.ConditionEntityId);

        }
    }
}
