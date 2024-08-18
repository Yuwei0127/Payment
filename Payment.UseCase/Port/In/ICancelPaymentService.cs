namespace Payment.UseCase.Port.In;

public interface ICancelPaymentService
{
    Task<bool> HandlerAsync(Guid paymentId, string failedReason);
}