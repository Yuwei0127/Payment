using Payment.Entities.Exceptions;
using Payment.SeedWork;
using Payment.SeedWork.Enum;

namespace Payment.Entities;

public class Payment : AggregateRoot<PaymentId>
{
    /// <summary>
    /// 訂單 Id
    /// </summary>
    public Guid OrderId { get; set; }
    
    /// <summary>
    /// 付款狀態
    /// </summary>
    public PaymentStatusEnum PaymentStatus { get; set; }

    /// <summary>
    /// 金額
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// 建立時間
    /// </summary>
    public DateTime CreateAt { get; set; }
    
    /// <summary>
    /// 付款失敗時間
    /// </summary>
    public DateTime? FailedAt { get; set; }
    
    /// <summary>
    /// 付款失敗原因
    /// </summary>
    public string? FailureReason { get; set; }
    
    /// <summary>
    /// 外部系統交易 Id
    /// </summary>
    public string? TransactionId { get; set; }
    

    protected override void When(DomainEvent @event)
    {
        switch (@event)
        {
            case AddPaymentEvent e :
                Id = Guid.NewGuid();
                OrderId = e.OrderId;
                PaymentStatus = PaymentStatusEnum.Pending;
                Amount = e.Amount;
                CreateAt = DateTime.Now;
                FailedAt = null;
                FailureReason = string.Empty;
                TransactionId = string.Empty;
                break;
            
            case CancelPaymentEvent e :
                PaymentStatus = PaymentStatusEnum.Failed;
                break;
        }
    }
    
    /// <summary>
    /// 參數驗證
    /// </summary>
    /// <exception cref="PaymentDomainException"></exception>
    protected override void EnsureValidState()
    {
        if (OrderId == Guid.Empty)
        {
            throw new PaymentDomainException("訂單編號不可為空");
        }
    }
}
