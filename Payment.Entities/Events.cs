using Payment.SeedWork;
using Payment.SeedWork.Enum;

namespace Payment.Entities;

/// <summary>
/// 新增付款事件
/// </summary>
public record AddPaymentEvent(Guid OrderId,decimal Amount) : DomainEvent;

/// <summary>
/// 取消付款事件
/// </summary>
/// <param name="PaymentId"></param>
public record CancelPaymentEvent(Guid PaymentId) : DomainEvent;









