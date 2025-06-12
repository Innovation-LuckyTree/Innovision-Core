using Microsoft.EntityFrameworkCore;
using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
{
  public void Configure(EntityTypeBuilder<Announcement> builder)
  {
    builder.ToTable("Announcement");
    builder.HasKey(e => e.AnnouncementId);

    builder.Property(e => e.AnnouncementId)
        .UseIdentityColumn(1, 1);

    builder.Property(e => e.IsBanner)
        .HasDefaultValue(0);

    builder.HasOne(e => e.Branch)
        .WithMany(f => f.Announcements)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
