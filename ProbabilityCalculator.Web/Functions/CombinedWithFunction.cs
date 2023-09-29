namespace ProbabilityCalculator.Functions;

public class CombinedWithFunction : IProbabilityFunction
{
    public string Name => "CombinedWith";
    
    public float Calculate(float a, float b)
    {
        return a * b;
    }
}