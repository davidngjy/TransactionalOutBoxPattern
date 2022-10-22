namespace TransactionalOutBoxPattern.Domain.Results;

public record SuccessfulResult : Result
{
}

public sealed record SuccessfulResult<T> : Result<T>
{
    public required T Content { get; set; }
}
