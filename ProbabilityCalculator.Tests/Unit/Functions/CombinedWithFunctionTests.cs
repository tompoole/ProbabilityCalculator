using ProbabilityCalculator.Functions;

namespace ProbabilityCalculator.Tests.Unit.Functions;

public class CombinedWithFunctionTests
{
    [Theory]
    [InlineData(0.5f, 0.5f, 0.25f)]
    [InlineData(0.5f, 0.25f, 0.125f)]
    [InlineData(0f, 0f, 0f)]
    [InlineData(1f, 1f, 1f)]
    public void Calculate_ShouldReturnCorrectValue(float a, float b, float expected)
    {
        // Arrange
        var function = new CombinedWithFunction();
        
        // Act
        var result = function.Calculate(a, b);
        
        // Assert
        Assert.Equal(expected, result);
    }
}