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

    public async Task<bool> HandlerAsync(Guid paymentId)
    {
        // 付款失敗
        var cancelStatus = PaymentStatusEnum.Failed;

        var success = await _paymentOutPort.ChangePaymentStatusAsync(paymentId, cancelStatus);
        
        // Log
        
        return success;
    }
}