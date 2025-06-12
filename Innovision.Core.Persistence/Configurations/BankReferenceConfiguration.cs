using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations
{
    public class BankReferenceConfiguration : IEntityTypeConfiguration<BankReference>
    {
        public void Configure(EntityTypeBuilder<BankReference> builder)
        {
            builder.ToTable("BankReference");
            builder.HasKey(e => new { e.ID });
        }
    }
}
