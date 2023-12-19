using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RuleBuilderInfra.Domain.Entities
{
    public class FieldOperatorJoiningEntityConfiguration : IEntityTypeConfiguration<FieldOperatorJoiningEntity>
    {
        public void Configure(EntityTypeBuilder<FieldOperatorJoiningEntity> builder)
        {
            builder.HasKey(e => new {e.OperatorTypeCode,e.FieldTypeCode });

            builder.HasOne(item=>item.FieldTypesEntity)
                   .WithMany(fieldTypeEntity => fieldTypeEntity.FieldOperatorJoiningEntities)
                   .HasForeignKey(fieldEntityType=> fieldEntityType.FieldTypeCode);

            builder.HasOne(operatorEntity => operatorEntity.OperatorTypesEntity)
                   .WithMany(operatorTypeEntity => operatorTypeEntity.FieldOperatorJoiningEntities)
                   .HasForeignKey(fieldOperatorEntity => fieldOperatorEntity.OperatorTypeCode);
        }
    }
}
