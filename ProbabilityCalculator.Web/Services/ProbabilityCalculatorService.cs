using ProbabilityCalculator.Functions;

namespace ProbabilityCalculator.Services;

public class ProbabilityCalculatorService : IProbabilityCalculatorService
{
    private readonly IEnumerable<IProbabilityFunction> _functions;
    private readonly ILogger<ProbabilityCalculatorService> _logger;

    public ProbabilityCalculatorService(IEnumerable<IProbabilityFunction> functions, ILogger<ProbabilityCalculatorService> logger)
    {
        _functions = functions;
        _logger = logger;
    }
    
    public IEnumerable<string> GetAllSupportedFunctions()
    {
        return _functions.Select(x => x.Name);
    }

    public ProbabilityCalculatorResult Calculate(string functionName, float a, float b)
    {
        var function = _functions.FirstOrDefault(x => string.Equals(x.Name, functionName, StringComparison.OrdinalIgnoreCase));
        if (function == null)
        {
            _logger.LogWarning("Specified function {FunctionName} could not be found.", functionName);
            return new ProbabilityCalculatorResult { Error = ProbabilityCalculatorError.FunctionNotFound };
        }
        
        if (a < 0 || a > 1 || b < 0 || b > 1)
        {
            _logger.LogWarning("Provided input values ({A}, {B}) are invalid.", a, b);
            return new ProbabilityCalculatorResult { Error = ProbabilityCalculatorError.InvalidInput };
        }

        var result = function.Calculate(a, b);
        _logger.LogInformation("Function {FunctionName} with inputs ({A}, {B}) resulted in {Result}.", functionName, a, b, result);
        
        return new ProbabilityCalculatorResult { Result = result };
    }
}