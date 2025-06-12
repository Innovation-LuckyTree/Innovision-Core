using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations
{
    public class AddressCodeConfiguration : IEntityTypeConfiguration<AddressCode>
    {
        public void Configure(EntityTypeBuilder<AddressCode> builder)
        {
            builder.ToTable("AddressCode");
            builder.HasKey(e => e.AddressCodeId);

            builder.Property(o => o.AddressCodeId)
                .UseIdentityColumn(1, 1);

            builder.HasOne(e => e.Account)
                .WithMany(f => f.AddressCodes)
                .HasForeignKey(e => e.AccountInfoId);
        }
    }
}
