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
        }
    }
}
