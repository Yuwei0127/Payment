using Payment.SeedWork;
using Payment.SeedWork.Enum;

namespace Payment.Entities;

/// <summary>
/// 新增付款事件
/// </summary>
public record RequestPaymentEvent(PaymentId Id, Guid OrderId,decimal Amount) : DomainEvent;

/// <summary>
/// 付款失敗事件
/// </summary>
public record PaymentFailedEvent(string FailedReason) : DomainEvent;

/// <summary>
/// 付款完成
/// </summary>
public record PaymentCompletedEvent(string TransactionId) : DomainEvent;









