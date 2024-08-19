using Payment.UseCase.Port.In;
using Payment.UseCase.Port.Out;

namespace Payment.UseCase;

public class CompletePaymentService : ICompletePaymentService
{
    private readonly IPaymentOutPort _paymentOutPort;

    public CompletePaymentService(IPaymentOutPort paymentOutPort)
    {
        _paymentOutPort = paymentOutPort;
    }

    public async Task<bool> HandleAsync(Guid paymentId, string transactionId)
    {
        var payment = await _paymentOutPort.GetAsync(paymentId);
        if (payment is null)
        {
            return false;
        }

        payment.PaymentCompleted(transactionId);

        var success = await _paymentOutPort.UpdateAsync(payment);

        return success;
    }
}