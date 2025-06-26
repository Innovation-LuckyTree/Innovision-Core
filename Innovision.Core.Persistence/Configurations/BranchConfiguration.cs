using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branch");
        builder.HasKey(e => e.BranchId);

        builder.Property(o => o.BranchId)
            .UseIdentityColumn(1, 1);

        builder.Property(e => e.BranchCreditObjectId);

        builder.Property(e => e.BranchBonusObjectId);

        builder.Property(e => e.BranchName)
            .HasMaxLength(300);

        builder.Property(e => e.BranchCode)
            .IsRequired(false);

        builder.Property(e => e.GameSiteManagerId)
            .IsRequired(false);

        builder.Property(e => e.GameSiteAccountId)
            .IsRequired(false);

        builder.Property(e => e.DefaultAccountId)
            .IsRequired(false);

        builder.Property(e => e.CreatedBy).HasMaxLength(100);

        builder.Property(e => e.LastModified);

        builder.Property(e => e.ModifiedBy).HasMaxLength(100);


        builder.HasOne(e => e.Address)
            .WithMany(f => f.Branches)
            .HasForeignKey(e => e.AddressId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
