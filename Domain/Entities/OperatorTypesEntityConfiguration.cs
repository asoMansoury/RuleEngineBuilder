using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RuleBuilderInfra.Domain.Entities
{
    public class OperatorTypesEntityConfiguration : IEntityTypeConfiguration<OperatorTypesEntity>
    {
        public void Configure(EntityTypeBuilder<OperatorTypesEntity> builder)
        {
            builder.HasKey(x => x.OperatorTypeCode);

            builder.Property(z => z.OperatorTypeCode)
                    .IsRequired();


            builder.HasMany(item => item.FieldOperatorJoiningEntities)
                   .WithOne(operatorJoiningEntity => operatorJoiningEntity.OperatorTypesEntity)
                   .HasForeignKey(operatorJoiningEntity => operatorJoiningEntity.OperatorTypeCode);
        }
    }
}
