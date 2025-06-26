using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class GameStatusConfiguration : IEntityTypeConfiguration<GameStatus>
{
    public void Configure(EntityTypeBuilder<GameStatus> builder)
    {
        builder.ToTable("GameStatus");
        builder.HasKey(e => e.GameStatusId);

        builder.Property(o => o.GameStatusId)
            .UseIdentityColumn(1, 1);

        builder.Property(e => e.Name)
            .IsRequired(false);

        builder.Property(e => e.Description)
            .IsRequired(false);

    }
}