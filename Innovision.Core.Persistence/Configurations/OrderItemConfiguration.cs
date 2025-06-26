// using Innovision.Core.Domain.Entity;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Innovision.Core.Persistence.Configurations;

// public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
// {
//     public void Configure(EntityTypeBuilder<OrderItem> builder)
//     {
//         builder.ToTable("OrderItem");
//         builder.HasKey(e => e.OrderItemId);

//         builder.Property(e => e.UsedDate)
//             .IsRequired(false);

//         builder.Property(e => e.AmountBet)
//             .IsRequired();

//         builder.Property(e => e.CompanyGameId)
//             .IsRequired();

//         builder.Property(e => e.DrawTime)
//             .IsRequired(false);
            
//         builder.Property(e => e.BetItemType)
//             .HasDefaultValue(0);

//         builder.Property(e => e.DrawDate)
//             .IsRequired(false);

//         builder.Property(e => e.IsBonus)
//             .HasDefaultValue(false);

//         builder.Property(e => e.IsDeleted)
//             .HasDefaultValue(false);

//         builder.HasOne(e => e.AccountInfo)
//             .WithMany(f => f.OrderItems)
//             .HasForeignKey(e => e.AccountInfoId);

//         builder.HasOne(e => e.Order)
//             .WithMany(f => f.OrderItems)
//             .HasForeignKey(e => e.OrderId);

//         // builder.HasOne(e => e.GameType)
//         //     .WithMany(f => f.OrderItems)
//         //     .HasForeignKey(e => e.GameTypeId);
//     }
// }
