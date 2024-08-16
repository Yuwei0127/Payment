namespace Payment.SeedWork;

public abstract record ValueObject<T> where T : ValueObject<T>;
