namespace TransactionalOutBoxPattern.Application.IntegrationEvents;

public abstract class IntegrationEvent<T> : IIntegrationEvent
{
    public Guid EventId { get; set; }

    public DateTimeOffset OccurredOn { get; set; }

    public T Content { get; set; }

    protected IntegrationEvent(T content)
    {
        Content = content;
    }
}
