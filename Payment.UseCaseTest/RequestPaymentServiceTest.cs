using System.Xml.Schema;
using FluentAssertions;
using NSubstitute;
using Payment.Entities;
using Payment.SeedWork;
using Payment.UseCase;
using Payment.UseCase.Port.In;
using Payment.UseCase.Port.Out;

namespace Payment.UseCaseTest;

public class RequestPaymentServiceTest
{
    private readonly IPaymentOutPort _paymentOutPort;
    private readonly IDomainEventBus _domainEventBus;

    public RequestPaymentServiceTest()
    {
        _domainEventBus = Substitute.For<IDomainEventBus>();;
        _paymentOutPort = Substitute.For<IPaymentOutPort>();
    }

    public IRequestPaymentService GetSystemUnderTest()
    {
        return new RequestPaymentService(_paymentOutPort, _domainEventBus);
    }

    [Fact]
    public async Task HandleAsyncTest_輸入OrderId_建立付款單成功_回傳True與發送事件()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        _paymentOutPort.SaveAsync(Arg.Any<Entities.Payment>()).Returns(true); 
        var amount = 100;
        
        // Act
        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(orderId, amount);

        // Assert
        _domainEventBus.Received(1).DispatchDomainEventsAsync(Arg.Any<Entities.Payment>());
    }
    
    [Fact]
    public async Task HandleAsyncTest_輸入OrderId_建立付款單失敗_回傳False()
    {
        var orderId = Guid.NewGuid();
        _paymentOutPort.SaveAsync(Arg.Any<Entities.Payment>()).Returns(false);
        var amount = 100;

        var sut = GetSystemUnderTest();
        var actual = await sut.HandleAsync(orderId, amount);

        actual.Should().Be(Guid.Empty);
    }
    
    
    
    
    
    
    
}


