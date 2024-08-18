namespace Payment.UseCase.Port.In;

public interface ICancelPaymentService
{
    Task<bool> HandleAsync(Guid paymentId, string failedReason);
}