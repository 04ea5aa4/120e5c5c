using LinkPage.Links.Classic;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Links.Classic
{
    public class TestGetLink
    {
        [Fact]
        public async Task GetLink_ReturnsStatusOK()
        {
            var client = new WebApplicationFactory<Program>().CreateClient();

            var response = await client.GetAsync("/v1/users/1/links/classic/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetLinks_ContainsExpectedLinks()
        {
            var expectedLink = new Model
            {
                Id = 1,
                Title = "Google",
                Url = "https://google.com",
            };
            var client = new WebApplicationFactory<Program>().CreateClient();

            var response = await client.GetAsync("/v1/users/1/links/classic/1");
            var serialisedBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var actualLink = JsonSerializer.Deserialize<Model>(serialisedBody, options);

            Assert.Equal(expectedLink, actualLink);
        }
    }
}