using Microsoft.EntityFrameworkCore;
using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
  public void Configure(EntityTypeBuilder<Notification> builder)
  {
    builder.ToTable("Notification");
    builder.HasKey(e => e.NotificationId);

    builder.Property(e => e.NotificationId)
        .UseIdentityColumn(1, 1);

    builder.Property(e => e.IsRead)
        .HasDefaultValue(0);

    builder.HasOne(e => e.NotificationType)
        .WithMany(f => f.Notifications)
        .HasForeignKey(e => e.NotificationTypeId);

    builder.HasOne(e => e.Account)
        .WithMany(f => f.Notifications)
        .HasForeignKey(e => e.AccountInfoId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}