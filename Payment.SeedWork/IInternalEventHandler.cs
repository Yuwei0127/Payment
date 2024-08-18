namespace Payment.SeedWork;

public interface IInternalEventHandler
{
    void Handle(DomainEvent domainEvent);
}