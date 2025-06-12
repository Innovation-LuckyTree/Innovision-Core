using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class GameTypeConfiguration : IEntityTypeConfiguration<GameType>
{
    public void Configure(EntityTypeBuilder<GameType> builder)
    {
        builder.ToTable("GameType");
        builder.HasKey(e => e.GameTypeId);

        builder.Property(o => o.GameTypeId)
            .UseIdentityColumn(1, 1);

        builder.Property(e => e.GameTypeName)
            .IsRequired(false);

        builder.Property(e => e.GameTypeDesciption)
            .IsRequired(false);

        builder.HasOne(o => o.Game)
            .WithMany(f => f.GameTypes)
            .HasForeignKey(e => e.GameId);
    }
}
