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
    /// 付款取消時間
    /// </summary>
    public DateTime? CancelAt { get; set; }
    
    /// <summary>
    /// 付款取消原因
    /// </summary>
    public string? CancelReason { get; set; }
    
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

    public Payment(PaymentId id, Guid orderId, decimal amount)
    {
        Apply(new RequestPaymentEvent(id, orderId, amount));
    }

    public void PaymentCancel(string cancelReason)
    {
        Apply(new PaymentCancelEvent(cancelReason));
    }
    
    public void PaymentFailed(string failedReason)
    {
        Apply(new PaymentFailedEvent(failedReason));
    }

    public void PaymentCompleted(string transactionId)
    {
        Apply(new PaymentCompletedEvent(transactionId));
    }
    
    protected override void When(DomainEvent @event)
    {
        switch (@event)
        {
            case RequestPaymentEvent e :
                Id = e.Id;
                OrderId = e.OrderId;
                PaymentStatus = PaymentStatusEnum.Pending;
                Amount = e.Amount;
                CreateAt = DateTime.Now;
                break;
            
            case PaymentCancelEvent e :
                PaymentStatus = PaymentStatusEnum.Cancel;
                CancelAt = DateTime.Now;
                CancelReason = e.CancelReason;
                break;
            
            case PaymentFailedEvent e :
                PaymentStatus = PaymentStatusEnum.Failed;
                FailedAt = DateTime.Now;
                FailureReason = e.FailedReason;
                break;
            
            case PaymentCompletedEvent e :
                PaymentStatus = PaymentStatusEnum.Completed;
                TransactionId = e.TransactionId;
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
