using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Game");
        builder.HasKey(e => e.GameId);

        builder.Property(o => o.GameId)
            .UseIdentityColumn(1, 1);

        builder.Property(e => e.GameObjectId);

        builder.Property(e => e.Name)
            .IsRequired(false);

        builder.Property(e => e.Description)
            .IsRequired(false);

        builder.Property(e => e.ExternalGameId)
            .IsRequired(false);

        builder.HasOne(e => e.GameProvider)
            .WithMany(f => f.Games)
            .HasForeignKey(e => e.GameProviderId);

        builder.HasOne(e => e.GameStatus)
            .WithMany(f => f.Games)
            .HasForeignKey(e => e.GameStatusId);

        builder.Property(e => e.CoverImage)
            .IsRequired(false);
    }
}
