﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace RuleBuilderInfra.Domain.Entities
{
    public class RuleEntityConfiguration : IEntityTypeConfiguration<RuleEntity>
    {
        public void Configure(EntityTypeBuilder<RuleEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(z=>z.QueryExpression) .IsRequired();

            // Add an index to enforce uniqueness
            builder.HasIndex(e => e.QueryExpression)
                   .IsUnique();

            builder.HasMany((condtion) => condtion.ConditionRulesEntity)
                    .WithOne((ruleItem) => ruleItem.RuleEntity)
                    .HasForeignKey((foreignKey) => foreignKey.RuleEntityId);

            builder.HasMany((childs) => childs.actionRuleEntities)
                .WithOne((parent) => parent.RuleEntity)
                .HasForeignKey(foreign => foreign.RuleEntityID);
        }
    }
}
