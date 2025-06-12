using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Configurations;

public class OTPConfiguration : IEntityTypeConfiguration<OTP>
{
    public void Configure(EntityTypeBuilder<OTP> builder)
    {
        builder.ToTable("OTP");
        builder.HasKey(e => e.OtpID);

        builder.Property(e => e.MobileNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Code)
            .HasMaxLength(6)
            .IsRequired();

        builder.Property(e => e.IsVerify)
            .HasDefaultValue(0);

        builder.Property(e => e.TransType)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.CreatedOn)
            .IsRequired();

        builder.Property(e => e.ExpireDate)
            .IsRequired();
    }
}
