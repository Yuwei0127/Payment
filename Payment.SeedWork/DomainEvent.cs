namespace Payment.SeedWork;

/// <summary>
/// 所有事件繼承基本屬性
/// </summary>
public record DomainEvent
{
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public Guid EventId { get; set; } = Guid.NewGuid();
}