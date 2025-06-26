using Microsoft.EntityFrameworkCore;
using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class GameAppVersionConfiguration : IEntityTypeConfiguration<GameAppVersion>
{
  public void Configure(EntityTypeBuilder<GameAppVersion> builder)
  {
    builder.ToTable("GameApplicationVersion");
    builder.HasKey(e => e.GameAppVersionId);

    builder.Property(e => e.GameAppVersionId)
        .UseIdentityColumn(1, 1);

    builder.Property(e => e.ReleaseNotes)
        .IsRequired(false);

    builder.HasOne(e => e.Game)
        .WithMany(f => f.GameAppVersions)
        .HasForeignKey(e => e.GameId);

    builder.HasOne(e => e.GameAppVersionStatus)
        .WithMany(f => f.GameAppVersions)
        .HasForeignKey(e => e.Status)
        .HasPrincipalKey(f => f.StatusId);
  }
}