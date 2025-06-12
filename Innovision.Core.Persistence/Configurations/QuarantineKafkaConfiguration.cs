using Microsoft.EntityFrameworkCore;
using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class QuarantineKafkaConfigueration : IEntityTypeConfiguration<QuarantineKafka>
{
  public void Configure(EntityTypeBuilder<QuarantineKafka> builder)
  {
    builder.ToTable("QuarantineKafka");
    builder.HasKey(e => e.QuarantineKafkaId);

    builder.Property(e => e.QuarantineKafkaId)
        .UseIdentityColumn(1, 1);
  }
}