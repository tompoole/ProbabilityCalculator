using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using ProbabilityCalculator.Functions;
using ProbabilityCalculator.Services;

namespace ProbabilityCalculator.Tests.Unit.Services;

public class ProbabilityCalculatorServiceTests
{
    [Fact]
    public void GetAllSupportedFunctions_ReturnsAllFunctions()
    {
        // Arrange
        ProbabilityCalculatorService sut = CreateSut();
        
        // Act
        var result = sut.GetAllSupportedFunctions().ToList();
        
        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains("FunctionA", result);
        Assert.Contains("FunctionB", result);
    }
    
    [Fact]
    public void Calculate_WithValidFunctionNameAndInputs_ReturnsCorrectResult()
    {
        // Arrange
        ProbabilityCalculatorService sut = CreateSut();
        
        // Act
        var result = sut.Calculate("FunctionA", 0.5f, 0.5f);
        
        // Assert
        Assert.Equal(0.5f, result.Result);
    }
    
    [Fact]
    public void Calculate_WithInvalidFunctionName_ReturnsError()
    {
        // Arrange
        ProbabilityCalculatorService sut = CreateSut();
        
        // Act
        var result = sut.Calculate("FunctionC", 0.5f, 0.5f);
        
        // Assert
        Assert.Equal(ProbabilityCalculatorError.FunctionNotFound, result.Error);
    }
    
    [Theory]
    [InlineData(0.5f, 1.5f)]
    [InlineData(-0.5f, 0.5f)]
    [InlineData(0.5f, -0.5f)]
    public void Calculate_WithInvalidInputs_ReturnsError(float a, float b)
    {
        // Arrange
        ProbabilityCalculatorService sut = CreateSut();
        
        // Act
        var result = sut.Calculate("FunctionA", a, b);
        
        // Assert
        Assert.Equal(ProbabilityCalculatorError.InvalidInput, result.Error);
    }
    
    private ProbabilityCalculatorService CreateSut()
    {
        var functionA = Substitute.For<IProbabilityFunction>();
        functionA.Calculate(Arg.Any<float>(), Arg.Any<float>()).Returns(0.5f);
        functionA.Name.Returns("FunctionA");
        
        var functionB = Substitute.For<IProbabilityFunction>();
        functionB.Calculate(Arg.Any<float>(), Arg.Any<float>()).Returns(1f);
        functionB.Name.Returns("FunctionB");

        var functions = new List<IProbabilityFunction>
        {
            functionA,
            functionB
        };

        return new ProbabilityCalculatorService(functions, new NullLogger<ProbabilityCalculatorService>());
    }
}