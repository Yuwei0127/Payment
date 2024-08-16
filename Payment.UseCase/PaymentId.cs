using Payment.SeedWork;

namespace Payment.UseCase;

public record PaymentId : ValueObject<PaymentId>
{
    private Guid Value { get; set; }

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
}