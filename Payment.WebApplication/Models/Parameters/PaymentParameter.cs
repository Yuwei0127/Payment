namespace Payment.WebApplication.Models.Parameters;

public class PaymentParameter
{
    public Guid OrderId { get; set; }

    public decimal Amount { get; set; }
}