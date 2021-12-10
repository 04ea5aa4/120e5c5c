using LinkPage;
using LinkPage.Links;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net.Http;

namespace IntegrationTests
{
    public static class WebApplicationFactoryExtensions
    {
        public static HttpClient CreateTestClient(this WebApplicationFactory<Program> factory, IEnumerable<Link> testData = null) =>
            factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services =>
                    {
                        if (testData is null)
                        {
                            testData = new List<Link>();
                        }
                        services.AddSingleton(new LinksRepository(testData));
                    }))
                .CreateClient();
    }
}
