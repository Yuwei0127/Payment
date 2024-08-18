using Payment.SeedWork;

namespace Payment.Entities;

public record PaymentId : ValueObject<PaymentId>
{
    public Guid Value { get; set; }

    public PaymentId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(value), "The value cannot be empty.");
        }

        Value = value;
    }

    public static implicit operator Guid(PaymentId self) => self.Value;
    public static implicit operator PaymentId(Guid self) => new(self);

    protected PaymentId()
    {
        
    }
}