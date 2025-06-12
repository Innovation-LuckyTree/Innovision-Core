using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class JackpotWinnerAttachmentConfiguration : IEntityTypeConfiguration<JackpotWinnerAttachment>
{
    public void Configure(EntityTypeBuilder<JackpotWinnerAttachment> builder)
    {
        builder.ToTable("JackpotWinnerAttachment");
        builder.HasKey(e => e.JackpotWinnerAttachmentId);

        builder.Property(e => e.JackpotWinnerAttachmentId)
            .UseIdentityColumn(1, 1);

        builder.Property(e => e.FileName)
            .IsRequired(true);

        builder.Property(e => e.FilePath)
            .IsRequired(true);

        builder.Property(e => e.FileType)
            .IsRequired(true);

        builder.HasOne(o => o.JackpotWinner)
            .WithMany(o => o.JackpotWinnerAttachments)
            .HasForeignKey(f => f.JackpotWinnerId);
    }
}