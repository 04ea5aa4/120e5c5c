using LinkPage;
using LinkPage.Links;
using LinkPage.Links.Classic;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net.Http;

namespace IntegrationTests
{
    public static class WebApplicationFactoryExtensions
    {
        public static HttpClient CreateTestClient(this WebApplicationFactory<Program> factory, IEnumerable<ClassicLink>? testData = null) =>
            factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services =>
                    {
                        if (testData is null)
                        {
                            testData = new List<ClassicLink>();
                        }
                        services.AddSingleton(new LinksRepository(testData));
                    }))
                .CreateClient();
    }
}
