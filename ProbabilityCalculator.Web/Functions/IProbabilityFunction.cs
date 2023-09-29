namespace ProbabilityCalculator.Functions;

public interface IProbabilityFunction
{
    public string Name { get; }
    public float Calculate(float a, float b);
}