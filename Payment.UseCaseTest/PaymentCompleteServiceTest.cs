using FluentAssertions;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;
using Payment.Entities.Exceptions;
using Payment.SeedWork;
using Payment.UseCase;
using Payment.UseCase.Port.In;
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

    public ICompletePaymentService GetSystemUnderTest()
    {
        return new CompletePaymentService(_paymentOutPort, _domainEventBus);
    }


    [Fact]
    public async Task HandleAsyncTest_輸入paymentId_完成付款成功_回傳True並回傳事件()
    {
        var paymentId = Guid.NewGuid();
        var orderId = Guid.NewGuid();
        var amount = 100;
        var transactionId = "DB2-XXX";
        var payment = new Entities.Payment(paymentId, orderId, amount);
        _paymentOutPort.GetAsync(paymentId).Returns(payment);
        _paymentOutPort.UpdateAsync(payment).Returns(true);

        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(paymentId, transactionId);

        actual.Should().BeTrue();
        _domainEventBus.Received(1).DispatchDomainEventsAsync(payment);
    }
    
    [Fact]
    public async Task HandleAsyncTest_輸入paymentId_完成付款失敗_回傳False()
    {
        var paymentId = Guid.NewGuid();
        var orderId = Guid.NewGuid();
        var amount = 100;
        var transactionId = "DB2-XXX";
        var payment = new Entities.Payment(paymentId, orderId, amount);
        _paymentOutPort.GetAsync(paymentId).Returns(payment);
        _paymentOutPort.UpdateAsync(payment).Returns(false);
        
        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(paymentId, transactionId);

        actual.Should().BeFalse();
    }

    [Fact]
    public async Task HandleAsyncTest_輸入paymentId_找不到付款資料_回傳PaymentDomainException例外()
    {
        var paymentId = Guid.NewGuid();
        var transactionId = "DB2-XXX";
        _paymentOutPort.GetAsync(paymentId).ReturnsNull();
        
        var sut = GetSystemUnderTest();
        var actual = async () => await sut.HandleAsync(paymentId, transactionId);
        
        await actual.Should().ThrowAsync<PaymentDomainException>();
    }
    
    
}






