using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class GameProviderConfiguration : IEntityTypeConfiguration<GameProvider>
{
    public void Configure(EntityTypeBuilder<GameProvider> builder)
    {
        builder.ToTable("GameProvider");
        builder.HasKey(e => e.GameProviderId);

        builder.Property(o => o.GameProviderId)
            .UseIdentityColumn(1, 1);

        builder.Property(e => e.Name)
            .IsRequired(false);

        builder.Property(e => e.Description)
            .IsRequired(false);

        builder.Property(e => e.CoverImage)
            .IsRequired(false);

    }
}