using System.Text.Json;
using FluentAssertions;

namespace ProbabilityCalculator.Tests.Integration;

[Collection(IntegrationCollection.Name)]
public class FunctionListTests 
{
    private readonly IntegrationTestFixture _fixture;

    public FunctionListTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Requesting_Function_List_Returns_Supported_Functions()
    {
        // Arrange
        var httpClient = _fixture.Client;
        
        // Act
        var response = await httpClient.GetAsync("/api/functions");
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        var functions = JsonSerializer.Deserialize<List<string>>(content);

        functions.Count.Should().Be(2);
        functions.Should().Contain("Either");
        functions.Should().Contain("CombinedWith");
    }
    
}