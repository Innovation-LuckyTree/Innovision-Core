using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class JackpotWinnerStatusConfiguration : IEntityTypeConfiguration<JackpotWinnerStatus>
{
    public void Configure(EntityTypeBuilder<JackpotWinnerStatus> builder)
    {
        builder.ToTable("JackpotWinnerStatus");
        builder.HasKey(e => e.JackpotWinnerStatusId);

        builder.Property(e => e.JackpotWinnerStatusId)
            .UseIdentityColumn(1, 1);

        builder.Property(e => e.Name)
            .IsRequired();

        builder.Property(e => e.Description)
            .IsRequired(false);
    }
}
