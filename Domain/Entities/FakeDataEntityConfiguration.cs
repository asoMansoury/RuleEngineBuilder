using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RuleBuilderInfra.Domain.Entities
{
    public class FakeDataEntityConfiguration : IEntityTypeConfiguration<FakeDataEntity>
    {
        public void Configure(EntityTypeBuilder<FakeDataEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

        }
    }
}
