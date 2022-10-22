namespace TransactionalOutBoxPattern.Domain.Exceptions;

public class TaskNotFoundException : DomainException
{
    public TaskNotFoundException(string message) : base(message) { }
}
