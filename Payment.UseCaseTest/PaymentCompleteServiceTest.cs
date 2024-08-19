using NSubstitute;
using Payment.SeedWork;
using Payment.UseCase.Port.Out;

namespace Payment.UseCaseTest;

public class PaymentCompleteServiceTest
{
    private readonly IPaymentOutPort _paymentOutPort;
    private readonly IDomainEventBus _domainEventBus;

    public PaymentCompleteServiceTest()
    {
        _domainEventBus = Substitute.For<IDomainEventBus>();
        _paymentOutPort = Substitute.For<IPaymentOutPort>();
    }


    [Fact]
    public async Task HandleAsyncTest_輸入paymentId_成功完成付款_回傳True()
    {
        var paymentId = Guid.NewGuid();
        
        
        
        
        
    }
    
    
    
    
    
    
}






