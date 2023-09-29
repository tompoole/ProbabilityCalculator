namespace ProbabilityCalculator.Tests.Integration;

[CollectionDefinition(Name)]
public class IntegrationCollection : ICollectionFixture<IntegrationTestFixture>
{
    public const string Name = nameof(IntegrationCollection);
}