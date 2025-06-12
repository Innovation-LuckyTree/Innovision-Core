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

        builder.Property(e => e.Region)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.Province)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.Municipality)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.Barangay)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.StreetOrPurok)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.PermanentRegion)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.PermanentProvince)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.PermanentMunicipality)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.PermanentBarangay)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.PermanentStreetOrPurok)
            .HasMaxLength(100)
            .IsRequired(false);
        builder.Property(e => e.CreatedBy).HasMaxLength(100);

        builder.Property(e => e.LastModified);

        builder.Property(e => e.ModifiedBy).HasMaxLength(100);
    }
}
