using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class GameCatalogConfiguration : IEntityTypeConfiguration<GameCatalog>
{
    public void Configure(EntityTypeBuilder<GameCatalog> builder)
    {
        builder.ToTable("GameCatalogs");
        builder.HasKey(e => e.GameCatalogId);

        builder.Property(o => o.GameCatalogId)
            .UseIdentityColumn(1, 1);

        builder.Property(e => e.GameId);
        builder.Property(e => e.GameCategoryId);

        builder.HasOne(g => g.Game)
            .WithMany(gc => gc.GameCatalogs)
            .HasForeignKey(g => g.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(gc => gc.GameCategory)
            .WithMany(g => g.GameCatalogs)
            .HasForeignKey(gc => gc.GameCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}