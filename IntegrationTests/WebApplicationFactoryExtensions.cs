using LinkPage.Links;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace IntegrationTests
{
    public static class WebApplicationFactoryExtensions
    {
        public static HttpClient CreateTestClient(this WebApplicationFactory<Program> factory, TestData testData) =>
            factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services =>
                    {
                        if (testData != null)
                        {
                            services.AddSingleton(new ClassicLinkRepository(testData.ClassicLinks));
                        }
                    }))
                .CreateClient();
    }
}
