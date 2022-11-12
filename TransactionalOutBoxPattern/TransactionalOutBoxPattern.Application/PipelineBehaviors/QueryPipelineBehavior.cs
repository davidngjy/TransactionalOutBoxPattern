using MediatR;
using TransactionalOutBoxPattern.Application.Abstraction;
using TransactionalOutBoxPattern.Application.Queries;

namespace TransactionalOutBoxPattern.Application.PipelineBehaviors;

internal class QueryPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public QueryPipelineBehavior(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (request
            .GetType()
            .GetInterfaces()
            .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IQuery<>))
           )
        {
            _unitOfWork.SetAsQueryMode();
        }

        return next();
    }
}
