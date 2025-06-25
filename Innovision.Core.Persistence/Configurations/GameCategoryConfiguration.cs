using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class GameCategoryConfiguration : IEntityTypeConfiguration<GameCategory>
{
    public void Configure(EntityTypeBuilder<GameCategory> builder)
    {
        builder.ToTable("GameCategory");
        builder.HasKey(e => e.GameCategoryId);

        builder.Property(o => o.GameCategoryId)
            .UseIdentityColumn(1, 1);

        builder.Property(e => e.Name)
            .IsRequired(false);

        builder.Property(e => e.Description)
            .IsRequired(false);

        builder.Property(e => e.CoverImage)
            .IsRequired(false);

    }
}