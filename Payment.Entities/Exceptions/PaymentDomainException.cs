namespace Payment.Entities.Exceptions;

public class PaymentDomainException : Exception
{
    public PaymentDomainException(string message) : base(message)
    {
        
    }
}