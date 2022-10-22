namespace TransactionalOutBoxPattern.Domain.Results;

public abstract record Result
{
    public static Result Successful => new SuccessfulResult();
}

public abstract record Result<T>
{
    public static Result<T> Successful(T content) => new SuccessfulResult<T>
    {
        Content = content
    };
}
