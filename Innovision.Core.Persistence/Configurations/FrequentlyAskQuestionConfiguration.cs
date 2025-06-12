using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class FrequentlyAskQuestionConfiguration : IEntityTypeConfiguration<FrequentlyAskQuestion>
{
    public void Configure(EntityTypeBuilder<FrequentlyAskQuestion> builder)
    {
        builder.ToTable("FrequentlyAskQuestion");
        builder.HasKey(e => e.FrequentlyAskQuestionId);

        builder.Property(e => e.FrequentlyAskQuestionId)
            .UseIdentityColumn(1, 1);

        builder.Property(e => e.Question)
            .IsRequired(true);

        builder.Property(e => e.Answer)
            .IsRequired(true);

        builder.HasOne(e => e.Game)
            .WithMany(f => f.FrequentlyAskQuestions)
            .HasForeignKey(f => f.GameId);
    }
}
