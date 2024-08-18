using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Payment.Adapter.Out.Configurations;

public class PaymentConfigurations : IEntityTypeConfiguration<Entities.Payment>
{
    public void Configure(EntityTypeBuilder<Entities.Payment> builder)
    {
        // 指定表名稱
        builder.ToTable("Payments");
        
        // 設定主鍵
        builder.HasKey(x => x.Id);
        
        // 設定 OrderId 屬性
        builder.Property(x => x.OrderId)
            .IsRequired();

        // 設定 PaymentStatus 屬性
        builder.Property(x => x.PaymentStatus)
            .IsRequired()
            .HasConversion<int>();

        // 設定 Amount 屬性
        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)"); // 設定金額的精度

        // 設定 CreateAt 屬性
        builder.Property(x => x.CreateAt)
            .IsRequired();

        // 設定 FailedAt 屬性
        builder.Property(x => x.FailedAt)
            .IsRequired(false);

        // 設定 FailureReason 屬性
        builder.Property(x => x.FailureReason)
            .HasMaxLength(500); // 假設失敗原因最大長度為 500

        // 設定 TransactionId 屬性
        builder.Property(x => x.TransactionId)
            .HasMaxLength(100); // 假設外部系統交易 ID 最大長度為 100
    }
}