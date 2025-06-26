// using Innovision.Core.Domain.Entity;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Innovision.Core.Persistence.Configurations;

// public class OrderConfiguration : IEntityTypeConfiguration<Order>
// {
//     public void Configure(EntityTypeBuilder<Order> builder)
//     {
//         builder.ToTable("Order");
//         builder.HasKey(e => e.OrderId);

//         builder.Property(e => e.TransactionNo)
//             .HasMaxLength(20)
//             .IsRequired(false);

//         builder.Property(e => e.CommissionStatusId)
//             .HasColumnType("int")
//             .HasDefaultValue(1);

//         builder.Property(e => e.IsBonus)
//             .HasDefaultValue(false);

//         builder.Property(e => e.IsDeleted)
//             .HasDefaultValue(false);

//         builder.HasOne(e => e.Game)
//             .WithMany(f => f.Orders)
//             .HasForeignKey(e => e.GameId);

//         builder.HasOne(e => e.AccountInfo)
//             .WithMany(f => f.Orders)
//             .HasForeignKey(e => e.AccountInfoId);
//     }
// }
