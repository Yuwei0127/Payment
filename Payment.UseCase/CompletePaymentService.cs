using Payment.Entities.Exceptions;
using Payment.SeedWork;
using Payment.UseCase.Port.In;
using Payment.UseCase.Port.Out;

namespace Payment.UseCase;

public class CompletePaymentService : ICompletePaymentService
{
    private readonly IPaymentOutPort _paymentOutPort;
    private readonly IDomainEventBus _domainEventBus;

    public CompletePaymentService(IPaymentOutPort paymentOutPort, IDomainEventBus domainEventBus)
    {
        _paymentOutPort = paymentOutPort;
        _domainEventBus = domainEventBus;
    }

    public async Task<bool> HandleAsync(Guid paymentId, string transactionId)
    {
        var payment = await _paymentOutPort.GetAsync(paymentId);
        if (payment is null)
        {
            throw new PaymentDomainException("無此付款單資訊");
        }

        payment.PaymentCompleted(transactionId);

        var success = await _paymentOutPort.UpdateAsync(payment);
        if (success)
        {
            // 發送事件
            await _domainEventBus.DispatchDomainEventsAsync(payment);
        }
        
        return success;
    }
}