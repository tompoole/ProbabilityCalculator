using ProbabilityCalculator.Services;
using System.Threading;

namespace ProbabilityCalculator.Web;

public static class Endpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/api/functions", (IProbabilityCalculatorService service) => service.GetAllSupportedFunctions());

        app.MapGet("/api/functions/{function}", (
            string function,
            float a,
            float b,
            IProbabilityCalculatorService service) =>
        {
            return service.Calculate(function, a, b) switch
            {
                { Error: ProbabilityCalculatorError.FunctionNotFound } => Results.NotFound($"Function {function} could not be found"),
                { Error: ProbabilityCalculatorError.InvalidInput } => Results.BadRequest("Inputs provided not valid. Must be between 0.0 and 1.0"),
                { Result: var result } => Results.Ok(result),
                _ => Results.StatusCode(500)
            };
        });
 
    }
    
}