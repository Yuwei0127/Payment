namespace Payment.UseCase.Port.In;

public interface ICreatePaymentService
{
    Task<Guid> HandlerAsync(Guid orderId,decimal amount);
}