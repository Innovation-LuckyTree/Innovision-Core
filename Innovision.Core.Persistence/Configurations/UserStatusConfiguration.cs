using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations
{
    public class UserStatusConfiguration : IEntityTypeConfiguration<UserStatus>
    {
        public void Configure(EntityTypeBuilder<UserStatus> builder)
        {
            builder.ToTable("UserStatus");
            builder.HasKey(e => e.UserStatusId);

            builder.Property(o => o.UserStatusId)
                .UseIdentityColumn(1, 1);

            builder.HasOne(e => e.Account)
                .WithMany(f => f.UserStatuses)
                .HasForeignKey(e => e.AccountInfoId);
        }
    }
}