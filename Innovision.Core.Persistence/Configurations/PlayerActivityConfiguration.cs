using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Persistence.Configurations
{
    public class PlayerActivityConfiguration : IEntityTypeConfiguration<PlayerActivity>
    {
        public void Configure(EntityTypeBuilder<PlayerActivity> builder)
        {
            builder.ToTable("PlayerActivity");
            builder.HasKey(e => e.ActivityId);

            builder.Property(o => o.ActivityId)
                .UseIdentityColumn(1, 1);

            builder.HasOne(e => e.Account)
                .WithMany(f => f.PlayerActivities)
                .HasForeignKey(e => e.AccountInfoId);
        }
    }
}