using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Configurations;

public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
{
    public void Configure(EntityTypeBuilder<UserType> builder)
    {
        builder.ToTable("UserType");
        builder.HasKey(e => new { e.UserTypeId });
        builder.Property(e => e.UserTypeId);
        builder.Property(e => e.UserTypeName).HasMaxLength(300);

        // Group Type = 0 - Dashboard, 1 - Accounting, 2 - Support
        // Role Type = 0 - admin , 1 - company, 2 - branch

    }
}
