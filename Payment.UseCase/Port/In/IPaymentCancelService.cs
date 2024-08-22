namespace Payment.UseCase.Port.In;

public interface IPaymentCancelService
{
    Task<bool> HandleAsync(Guid paymentId, string cancelReason);
}