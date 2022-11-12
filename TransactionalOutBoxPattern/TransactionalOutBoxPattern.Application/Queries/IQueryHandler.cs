using MediatR;
using TransactionalOutBoxPattern.Domain.Results;

namespace TransactionalOutBoxPattern.Application.Queries;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
