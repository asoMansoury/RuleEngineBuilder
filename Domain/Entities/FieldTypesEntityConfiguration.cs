using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RuleBuilderInfra.Domain.Entities
{
    public class FieldTypesEntityConfiguration : IEntityTypeConfiguration<FieldTypesEntity>
    {
        public void Configure(EntityTypeBuilder<FieldTypesEntity> builder)
        {
            builder.HasKey(z => z.FieldTypeCode);
            builder.Property(z => z.FieldTypeCode)
                .IsRequired();

            builder.HasIndex(z => z.FieldTypeCode)
                .IsUnique();

            builder.HasMany(item => item.FieldOperatorJoiningEntities)
                   .WithOne(operatorJoiningEntity => operatorJoiningEntity.FieldTypesEntity)
                   .HasForeignKey(operatorJoiningEntity => operatorJoiningEntity.FieldTypeCode);
        }
    }
}
