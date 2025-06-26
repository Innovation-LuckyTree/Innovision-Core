using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class DrawResultConfiguration : IEntityTypeConfiguration<DrawResult>
{
    public void Configure(EntityTypeBuilder<DrawResult> builder)
    {
        builder.ToTable("DrawResult");
        builder.HasKey(e => e.DrawResultId);

        builder.Property(e => e.DrawResultId)
            .UseIdentityColumn(1, 1);

        builder.Property(e => e.RoundReference)
            .IsRequired(false);

        builder.HasOne(e => e.Game)
            .WithMany(f => f.DrawResults)
            .HasForeignKey(e => e.GameId);
    }
}
