namespace TransactionalOutBoxPattern.Application.IntegrationEvents;

public class MessageIntegrationEvent<T> : IntegrationEvent<T>
{
    public MessageIntegrationEvent(T content) : base(content)
    {
    }
}
