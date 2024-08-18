using Payment.SeedWork.Enum;
using Payment.UseCase.Port.In;
using Payment.UseCase.Port.Out;

namespace Payment.UseCase;

public class CreatePaymentService : ICreatePaymentService
{
    private readonly IPaymentOutPort _paymentOutPort;

    public CreatePaymentService(IPaymentOutPort paymentOutPort)
    {
        _paymentOutPort = paymentOutPort;
    }

    public async Task<Guid> HandlerAsync(Guid orderId)
    {
        var newPaymentId = Guid.NewGuid();

        var success = await _paymentOutPort.SaveAsync(new Entities.Payment
        {
            OrderId = newPaymentId,
            PaymentStatus = PaymentStatusEnum.Pending,
            CreateAt = DateTime.Now,
            FailedAt = null,
            FailureReason = null,
            TransactionId = null
        });

        return success ? newPaymentId : Guid.Empty;
    }
}