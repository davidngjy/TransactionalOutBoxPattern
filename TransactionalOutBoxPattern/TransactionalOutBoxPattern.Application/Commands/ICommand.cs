using MediatR;
using TransactionalOutBoxPattern.Domain.Results;

namespace TransactionalOutBoxPattern.Application.Commands;

internal interface ICommand : IRequest<Result>
{
}

internal interface ICommand<T> : IRequest<Result<T>>
{
}
