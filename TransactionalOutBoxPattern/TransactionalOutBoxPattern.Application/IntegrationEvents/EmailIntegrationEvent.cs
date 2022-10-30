namespace TransactionalOutBoxPattern.Application.IntegrationEvents;

public class EmailIntegrationEvent<T> : IntegrationEvent<T>
{
    public EmailIntegrationEvent(T content) : base(content)
    {
    }
}
