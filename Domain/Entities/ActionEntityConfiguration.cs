using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RuleBuilderInfra.Domain.Entities
{
    public class ActionEntityConfiguration : IEntityTypeConfiguration<ActionEntity>
    {
        public void Configure(EntityTypeBuilder<ActionEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(z => z.ServiceAssembly)
                    .IsRequired();

            builder.Property(z => z.ServiceName)
            .IsRequired();

            builder.Property(z => z.CategoryService)
            .IsRequired();

            builder.HasMany((childs) => childs.ActionPropertis)
                .WithOne((parent) => parent.ActionEntity)
                .HasForeignKey(foreign => foreign.ActionEntityID);

            builder.HasMany((childs) => childs.actionRuleEntities)
                    .WithOne((parent) => parent.ActionEntity)
                    .HasForeignKey(foreign => foreign.ActionEntityID);

        }
    }
}
