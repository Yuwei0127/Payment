using Payment.Entities;
using Payment.SeedWork;
using Payment.SeedWork.Enum;
using Payment.UseCase.Port.In;
using Payment.UseCase.Port.Out;

namespace Payment.UseCase;

public class RequestPaymentService : IRequestPaymentService
{
    private readonly IPaymentOutPort _paymentOutPort;
    private readonly IDomainEventBus _domainEventBus;

    public RequestPaymentService(IPaymentOutPort paymentOutPort, IDomainEventBus domainEventBus)
    {
        _paymentOutPort = paymentOutPort;
        _domainEventBus = domainEventBus;
    }

    public async Task<Guid> HandleAsync(Guid orderId,decimal amount)
    {
        var paymentId = new PaymentId(Guid.NewGuid());
        var newPayment = new Entities.Payment(paymentId, orderId, amount);

        var success = await _paymentOutPort.SaveAsync(newPayment);
        if (success.Equals(false))
        {
            return Guid.Empty;
        }

        // 發送事件
        await _domainEventBus.DispatchDomainEventsAsync(newPayment);
        return newPayment.Id;
    }
}