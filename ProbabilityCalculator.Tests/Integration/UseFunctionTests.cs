using System.Net;
using FluentAssertions;

namespace ProbabilityCalculator.Tests.Integration;

[Collection(IntegrationCollection.Name)]
public class UseFunctionTests
{
    private readonly IntegrationTestFixture _fixture;

    public UseFunctionTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }
    
    [Fact]
    public async Task Requesting_Function_With_Invalid_Parameters_Returns_Bad_Request()
    {
        // Arrange
        var httpClient = _fixture.Client;
        
        // Act
        var response = await httpClient.GetAsync("/api/functions/CombinedWith?a=0.5&b=1.5");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Requesting_Function_With_Valid_Parameters_Returns_Correct_Result()
    {
        // Arrange
        var httpClient = _fixture.Client;
        
        // Act
        var response = await httpClient.GetAsync("/api/functions/CombinedWith?a=0.5&b=0.5");
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        var isFloat = float.TryParse(content, out var result);
        
        isFloat.Should().BeTrue();
        result.Should().Be(0.25f);
    }
    
    [Fact]
    public async Task Requesting_Function_With_Invalid_Function_Name_Returns_Not_Found()
    {
        // Arrange
        var httpClient = _fixture.Client;
        
        // Act
        var response = await httpClient.GetAsync("/api/functions/NotAFunction?a=0.5&b=0.5");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
