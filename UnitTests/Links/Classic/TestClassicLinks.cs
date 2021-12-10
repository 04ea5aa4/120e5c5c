using LinkPage.Links.Classic;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Links.Classic
{
    public class TestClassicLinks
    {
        [Fact]
        public async Task GetLinks_ReturnsStatusOK()
        {
            var client = new WebApplicationFactory<Program>().CreateClient();

            var response = await client.GetAsync("/v1/users/1/links/classic");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetLinks_ContainsExpectedLinks()
        {
            var expectedLinks = new List<Model>()
            {
                new Model
                {
                    Title = "Google",
                    Url = "https://google.com",
                },
                new Model
                {
                    Title = "Facebook",
                    Url = "https://facebook.com",
                },
            };
            var client = new WebApplicationFactory<Program>().CreateClient();

            var response = await client.GetAsync("/v1/users/1/links/classic");
            var serialisedBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var actualLinks = JsonSerializer.Deserialize<IEnumerable<Model>>(serialisedBody, options);

            Assert.Equal(expectedLinks, actualLinks);
        }
    }
}