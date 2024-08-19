using Payment.Entities.Exceptions;
using Payment.SeedWork.Enum;
using Payment.UseCase.Port.In;
using Payment.UseCase.Port.Out;

namespace Payment.UseCase;

public class CancelPaymentService : ICancelPaymentService
{
    private readonly IPaymentOutPort _paymentOutPort;

    public CancelPaymentService(IPaymentOutPort paymentOutPort)
    {
        _paymentOutPort = paymentOutPort;
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
        
        return success;
    }
}