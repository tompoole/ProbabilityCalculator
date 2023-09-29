using ProbabilityCalculator.Functions;

namespace ProbabilityCalculator.Tests.Unit.Functions;

public class EitherFunctionTests
{
    [Theory]
    [InlineData(0.5f, 0.5f, 0.75f)]
    [InlineData(0f, 0f, 0f)]
    [InlineData(1f, 1f, 1f)]
    [InlineData(0.1f, 0.9, 0.91f)]
    public void Calculate_ShouldReturnCorrectValue(float a, float b, float expected)
    {
        // Arrange
        var function = new EitherFunction();
        
        // Act
        var result = function.Calculate(a, b);
        
        // Assert
        Assert.Equal(expected, result);
    }
}