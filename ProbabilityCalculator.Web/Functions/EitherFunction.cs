namespace ProbabilityCalculator.Functions;

public class EitherFunction : IProbabilityFunction
{
    public string Name => "Either";
    
    public float Calculate(float a, float b)
    {
        return a + b - a * b;
    }
}