using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class WithdrawalStatusConfiguration : IEntityTypeConfiguration<WithdrawalStatus>
{
    public void Configure(EntityTypeBuilder<WithdrawalStatus> builder)
    {
        builder.ToTable("WithdrawalStatus");
        builder.HasKey(e => e.WithdrawalStatusId);

        builder.Property(e => e.WithdrawalStatusId)
            .UseIdentityColumn(1, 1);

        builder.Property(e => e.Name)
            .IsRequired();

        builder.Property(e => e.Description)
            .IsRequired(false);
    }
}
