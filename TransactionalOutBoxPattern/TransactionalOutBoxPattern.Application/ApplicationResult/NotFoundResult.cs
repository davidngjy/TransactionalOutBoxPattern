using TransactionalOutBoxPattern.Domain.Results;

namespace TransactionalOutBoxPattern.Application.ApplicationResult;

public record NotFoundResult : Result
{
    public static Result Create() => new NotFoundResult();
}

public record NotFoundResult<T> : Result<T>
{
    public static Result<T> Create() => new NotFoundResult<T>();
}
