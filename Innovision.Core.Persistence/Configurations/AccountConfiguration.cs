using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("AccountInfo");
        builder.HasKey(e => e.AccountInfoId);

        builder.Property(e => e.AccountObjectId);
        builder.Property(e => e.UserId);
        builder.Property(e => e.AccountCreditId);
        builder.Property(e => e.AccountBonusId);
        builder.Property(e => e.BranchId);

        builder.Property(e => e.FirstName)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(e => e.LastName)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(e => e.Commision)
            .HasColumnType("decimal(10, 4)");//.HasPrecision(10, 2); // to confirm

        builder.Property(e => e.MiddleName)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(e => e.Suffix)
            .HasMaxLength(5)
            .IsRequired(false);

        builder.Property(e => e.Email)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(e => e.Gender)
            .HasMaxLength(6)
            .IsRequired(false);

        builder.Property(e => e.MartialStatus)
            .HasMaxLength(30)
            .IsRequired(false);

        //builder.Property(e => e.UserStatus)
        //    .IsRequired(false);

        builder.Property(e => e.SalaryRange)
            .IsRequired(false);

        builder.Property(e => e.BloodType)
            .HasMaxLength(10)
            .IsRequired(false);

        builder.Property(e => e.PaymentAccountId)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.Property(e => e.PlaceOfBirth)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(e => e.BirthDate)
            .HasMaxLength(10)
            .IsRequired(false);

        builder.Property(e => e.Nationality)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(e => e.NatureOfWork)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(e => e.SourceOfIncome)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(e => e.MobileNumber)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(e => e.RefferralCode)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.ValidId)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.FrontIdPath)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.BackIdPath)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.SignaturePath)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.ProfilePath)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.SelfiePath)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.AccountCommission)
            .HasMaxLength(2500)
            .IsRequired(false);

        builder.Property(e => e.RefferralKey)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(e => e.IsActive)
            .HasDefaultValue(0);

        builder.Property(e => e.IsDeclined)
            .IsRequired(false)
            .HasDefaultValue(false);

        builder.Property(e => e.CreatedOn)
            .IsRequired();

        builder.Property(e => e.CreatedBy)
            .HasMaxLength(100);

        builder.Property(e => e.LastModified);

        builder.Property(e => e.ModifiedBy)
            .HasMaxLength(100);

        builder.Property(e => e.Remarks)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.LastSetPassword);

        builder.Property(e => e.IsMain)
            .HasDefaultValue(0);

        builder.HasOne(e => e.Branch)
            .WithMany(f => f.Account)
            .HasForeignKey(e => e.BranchId);

        builder.HasOne(e => e.UserType)
            .WithMany(f => f.Accounts)
            .HasForeignKey(e => e.UserTypeId);

        builder.HasOne(e => e.PresentAddress)
            .WithMany(f => f.PresentAccountAddresses)
            .HasForeignKey(e => e.PresentAddressId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.PermanentAddress)
            .WithMany(f => f.PermanentAccountAddresses)
            .HasForeignKey(e => e.PermanentAddressId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
