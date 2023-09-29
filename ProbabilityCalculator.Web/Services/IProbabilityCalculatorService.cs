namespace ProbabilityCalculator.Services;

public interface IProbabilityCalculatorService
{
    public IEnumerable<string> GetAllSupportedFunctions();
    public ProbabilityCalculatorResult Calculate(string functionName, float a, float b);
}