using MediatR;
using System.Text.Json;

namespace TransactionalOutBoxPattern.Domain.DomainEvents;

public interface IDomainEvent : INotification
{
    string TypeName => GetType().AssemblyQualifiedName!;

    string SerializedContent => JsonSerializer.Serialize(this, GetType());
}
