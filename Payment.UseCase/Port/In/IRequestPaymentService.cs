using Payment.Entities;

namespace Payment.UseCase.Port.In;

public interface IRequestPaymentService
{
    Task<Guid> HandleAsync(Guid orderId,decimal amount);
}