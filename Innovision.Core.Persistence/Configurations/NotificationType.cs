using Microsoft.EntityFrameworkCore;
using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class NotificationTypeConfiguration : IEntityTypeConfiguration<NotificationType>
{
  public void Configure(EntityTypeBuilder<NotificationType> builder)
  {
    builder.ToTable("NotificationType");
    builder.HasKey(e => e.NotificationTypeId);

    builder.Property(e => e.NotificationTypeId)
        .UseIdentityColumn(1, 1);

    builder.Property(e => e.Title)
        .IsRequired();

    builder.Property(e => e.Description)
        .IsRequired(false);
  }
}