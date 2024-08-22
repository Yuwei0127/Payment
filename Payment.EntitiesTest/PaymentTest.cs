using System.Diagnostics;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;
using Payment.Entities.Exceptions;
using Payment.SeedWork.Enum;

namespace Payment.EntitiesTest;

public class PaymentTest
{
    [Fact]
    public void RequestPaymentTest_InputIdAndOrderIdAndAmount_ShouldCreatePayment()
    {
        var paymentId = Guid.NewGuid();
        var orderId = Guid.NewGuid();
        var amount = 100;

        var actual = new Entities.Payment(paymentId, orderId, amount);

        actual.Id.Value.Should().Be(paymentId);
        actual.OrderId.Should().Be(orderId);
        actual.PaymentStatus.Should().Be(PaymentStatusEnum.Pending);
        actual.Amount.Should().Be(amount);
    }

    [Fact]
    public void PaymentFailed_InputReason_ShouldFailed()
    { 
        var paymentId = Guid.NewGuid();
        var orderId = Guid.NewGuid();
        var amount = 100;

        var payment = new Entities.Payment(paymentId, orderId, amount);
        var reason = "訂單過期";
        
        payment.PaymentFailed(reason);

        payment.PaymentStatus.Should().Be(PaymentStatusEnum.Failed);
        payment.FailureReason.Should().Be(reason);
    }

    [Fact]
    public void PaymentCompleted_InputTransactionId_ShouldComplete()
    {
        var paymentId = Guid.NewGuid();
        var orderId = Guid.NewGuid();
        var amount = 100;
        
        var payment = new Entities.Payment(paymentId, orderId, amount);

        var transactionId = "DB2-XXX";

        payment.PaymentCompleted(transactionId);

        payment.PaymentStatus.Should().Be(PaymentStatusEnum.Completed);
        payment.TransactionId.Should().Be(transactionId);
    }

    [Fact]
    public void PaymentEnsureValidState_InputOrderIdEmpty_ShouldThrowPaymentDomainException()
    {
        var paymentId = Guid.NewGuid();
        var orderId = Guid.Empty;
        var amount = 100;

        var exception = Assert.Throws<PaymentDomainException>(() => new Entities.Payment(paymentId, orderId, amount));
        
        Assert.Equal("訂單編號不可為空",exception.Message);
    }
}




