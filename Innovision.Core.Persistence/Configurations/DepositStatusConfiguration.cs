using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class DepositStatusConfiguration : IEntityTypeConfiguration<DepositStatus>
{
    public void Configure(EntityTypeBuilder<DepositStatus> builder)
    {
        builder.ToTable("DepositStatus");
        builder.HasKey(e => e.DepositStatusId);

        builder.Property(e => e.DepositStatusId)
            .UseIdentityColumn(1, 1);

        builder.Property(e => e.Name)
            .IsRequired();

        builder.Property(e => e.Description)
            .IsRequired(false);
    }
}
