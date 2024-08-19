namespace Payment.UseCase.Port.In;

public interface IPaymentFailedService
{
    Task<bool> HandleAsync(Guid paymentId, string failedReason);
}