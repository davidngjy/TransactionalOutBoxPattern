using MediatR;
using System.Text.Json;

namespace TransactionalOutBoxPattern.Application.IntegrationEvents;

public interface IIntegrationEvent : IRequest
{
    string TypeName => GetType().AssemblyQualifiedName!;

    string SerializedContent => JsonSerializer.Serialize(this, GetType());
}
