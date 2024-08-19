using Payment.SeedWork;

namespace Payment.EventBus;

public class DefaultEventBus : IDomainEventBus
{
    public Task DispatchDomainEventsAsync<TId>(AggregateRoot<TId> aggregateRoot) where TId : ValueObject<TId>
    {
        return Task.CompletedTask;
    }
}