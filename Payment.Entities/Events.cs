using Payment.SeedWork;
using Payment.SeedWork.Enum;

namespace Payment.Entities;

/// <summary>
/// 新增付款事件
/// </summary>
public record CreatePaymentEvent(Guid OrderId,decimal Amount) : DomainEvent;

/// <summary>
/// 付款失敗事件
/// </summary>
public record PaymentFailedEvent(Guid PaymentId,string FailedReason) : DomainEvent;









