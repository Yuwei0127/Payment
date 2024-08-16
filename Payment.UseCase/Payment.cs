using Payment.SeedWork.Enum;

namespace Payment.UseCase;

public class Payment
{
    /// <summary>
    /// 付款 Id
    /// </summary>
    public Guid PaymentId { get; set; }
    
    /// <summary>
    /// 訂單 Id
    /// </summary>
    public Guid OrderId { get; set; }
    
    /// <summary>
    /// 付款狀態
    /// </summary>
    public PaymentStatusEnum PaymentStatus { get; set; }
    
    /// <summary>
    /// 建立時間
    /// </summary>
    public DateTime CreateAt { get; set; }
    
    /// <summary>
    /// 付款失敗時間
    /// </summary>
    public DateTime FailedAt { get; set; }
    
    /// <summary>
    /// 付款失敗原因
    /// </summary>
    public string? FailureReason { get; set; }
    
    /// <summary>
    /// 外部系統交易 Id
    /// </summary>
    public string? TransactionId { get; set; }
}
