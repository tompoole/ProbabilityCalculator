namespace ProbabilityCalculator.Services;

public record ProbabilityCalculatorResult
{
    public float? Result { get; init; }
    public bool IsSuccess => Result.HasValue;
    public ProbabilityCalculatorError? Error { get; init; }
}

public enum ProbabilityCalculatorError
{
    FunctionNotFound,
    InvalidInput
}