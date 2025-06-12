using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations
{
    public class UserTypeConfigConfiguration : IEntityTypeConfiguration<UserTypeConfig>
    {
        public void Configure(EntityTypeBuilder<UserTypeConfig> builder)
        {
            builder.ToTable("UserTypeConfig");
            builder.HasKey(e => e.Id);

            builder.Property(o => o.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(e => e.RequestCredit)
            .IsRequired(false);

            builder.Property(e => e.CashinDeposit)
            .IsRequired(false);

            builder.HasOne(e => e.UserType)
                .WithMany(f => f.UserTypeAccessControls)
                .HasForeignKey(e => e.UserTypeId);
        }
    }
}
