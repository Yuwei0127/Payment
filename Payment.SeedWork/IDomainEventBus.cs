namespace Payment.SeedWork;

public interface IDomainEventBus
{
    Task DispatchDomainEventsAsync<TId>(AggregateRoot<TId> aggregateRoot) where TId : ValueObject<TId>;
}