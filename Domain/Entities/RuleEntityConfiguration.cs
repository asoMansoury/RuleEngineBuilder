using Microsoft.EntityFrameworkCore;
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
        }
    }
}
