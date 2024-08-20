using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Payment.Entities.Exceptions;
using Payment.SeedWork;
using Payment.SeedWork.Enum;
using Payment.UseCase;
using Payment.UseCase.Port.In;
using Payment.UseCase.Port.Out;

namespace Payment.UseCaseTest;

public class PaymentFailedServiceTest
{
    private readonly IPaymentOutPort _paymentOutPort;
    private readonly IDomainEventBus _domainEventBus;

    public PaymentFailedServiceTest()
    {
        _paymentOutPort = Substitute.For<IPaymentOutPort>();
        _domainEventBus = Substitute.For<IDomainEventBus>();
    }

    public IPaymentFailedService GetSystemUnderTest()
    {
        return new PaymentFailedFailedService(_paymentOutPort, _domainEventBus);
    }
    
    [Fact]
    public async Task HandleAsyncTest_輸入PaymentId_找不到付款資料_回傳PaymentDomainException例外()
    {
        var paymentId = Guid.NewGuid();
        var failedReason = "訂單到期";
        _paymentOutPort.GetAsync(paymentId).ReturnsNull();

        var sut = GetSystemUnderTest();
        var actual = async () => await sut.HandleAsync(paymentId, failedReason);

        await actual.Should().ThrowAsync<PaymentDomainException>();
    }
    
    

    [Fact]
    public async Task HandleAsyncTest_輸入OrderId_付款單狀態改為失敗_回傳True()
    {
        var orderId = Guid.NewGuid();
        var paymentId = Guid.NewGuid();
        var amount = 100;
        var failedReason = "訂單到期";
        var payment = new Entities.Payment(paymentId, orderId, amount);
        _paymentOutPort.GetAsync(paymentId).Returns(payment);
        _paymentOutPort.UpdateAsync(payment).Returns(true);

        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(paymentId, failedReason);

        actual.Should().BeTrue();
        _domainEventBus.Received(1).DispatchDomainEventsAsync(payment);
    }
    
    [Fact]
    public async Task HandleAsyncTest_輸入OrderId_付款單狀態更改失敗_回傳False()
    {
        var orderId = Guid.NewGuid();
        var amount = 100;
        var paymentId = Guid.NewGuid();
        var failedReason = "訂單到期";
        var payment = new Entities.Payment(paymentId, orderId, amount);
        _paymentOutPort.GetAsync(paymentId).Returns(payment);
        _paymentOutPort.UpdateAsync(payment).Returns(false);

        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(paymentId, failedReason);

        actual.Should().BeFalse();
    }
}




