using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RuleBuilderInfra.Domain.Entities
{
    public class ActionPropertiesEntityConfiguration : IEntityTypeConfiguration<ActionPropertiesEntity>
    {
        public void Configure(EntityTypeBuilder<ActionPropertiesEntity> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.HasOne((parent) => parent.ActionEntity)
                .WithMany((parent) => parent.ActionPropertis)
                .HasForeignKey(foreign => foreign.ActionEntityID);
        }
    }
}
