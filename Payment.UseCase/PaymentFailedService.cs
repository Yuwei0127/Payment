using Payment.Entities.Exceptions;
using Payment.SeedWork;
using Payment.SeedWork.Enum;
using Payment.UseCase.Port.In;
using Payment.UseCase.Port.Out;

namespace Payment.UseCase;

public class PaymentFailedService : IPaymentFailedService
{
    private readonly IPaymentOutPort _paymentOutPort;
    private readonly IDomainEventBus _domainEventBus;

    public PaymentFailedService(IPaymentOutPort paymentOutPort, IDomainEventBus domainEventBus)
    {
        _paymentOutPort = paymentOutPort;
        _domainEventBus = domainEventBus;
    }

    public async Task<bool> HandleAsync(Guid paymentId, string failedReason)
    {
        var payment = await _paymentOutPort.GetAsync(paymentId);
        if (payment is null)
        {
            throw new PaymentDomainException("無此付款單資訊");
        }

        payment.PaymentFailed(failedReason);
        
        var success = await _paymentOutPort.UpdateAsync(payment);
        if (success)
        {
            await _domainEventBus.DispatchDomainEventsAsync(payment);
        }
        
        return success;
    }
}