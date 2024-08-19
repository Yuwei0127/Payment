using Payment.Entities;

namespace Payment.UseCase.Port.In;

public interface ICompletePaymentService
{
    Task<bool> HandleAsync(Guid paymentId, string transactionId);
}