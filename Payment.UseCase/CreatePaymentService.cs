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

    public async Task<Guid> HandlerAsync(Guid orderId,decimal amount)
    {
        var newPayment = new Entities.Payment(orderId, amount);

        var success = await _paymentOutPort.SaveAsync(newPayment);

        return success ? newPayment.Id : Guid.Empty;
    }
}