using Microsoft.EntityFrameworkCore;
using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class LiveStreamConfiguration : IEntityTypeConfiguration<LiveStream>
{
    public void Configure(EntityTypeBuilder<LiveStream> builder)
    {
        builder.ToTable("LiveStream");
        builder.HasKey(e => e.LiveStreamId);

        builder.Property(e => e.LiveStreamId)
            .UseIdentityColumn(1, 1);

        builder.Property(e => e.Title)
            .IsRequired();

        builder.Property(e => e.Link)
            .IsRequired();

        builder.Property(e => e.Description)
            .IsRequired(false);

        builder.HasOne(e => e.Game)
            .WithMany(f => f.LiveStreams)
            .HasForeignKey(e => e.GameId);
    }
}