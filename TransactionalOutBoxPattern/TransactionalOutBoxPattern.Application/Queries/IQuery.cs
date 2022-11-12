using MediatR;
using TransactionalOutBoxPattern.Domain.Results;

namespace TransactionalOutBoxPattern.Application.Queries;

public interface IQuery<T> : IRequest<Result<T>>
{
}
