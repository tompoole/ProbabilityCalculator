using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace ProbabilityCalculator.Tests.Integration;

public class IntegrationTestFixture : IDisposable
{
    private readonly WebApplicationFactory<Program> _application;

    public IntegrationTestFixture()
    {
        _application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // You could add / replace services here
                });
            });

        Client = _application.CreateClient();
    }

    public HttpClient Client { get; }

    public void Dispose()
    {
        _application.Dispose();
    }
}