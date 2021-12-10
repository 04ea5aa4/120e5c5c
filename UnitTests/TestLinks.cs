using LinkPage;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class TestLinks
    {
        [Fact]
        public async Task GetLinks_ReturnsStatusOK()
        {
            var client = new WebApplicationFactory<Program>().CreateClient();

            var response = await client.GetAsync("/v1/users/1/links");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetLinks_ContainsExpectedLinks()
        {
            var expectedLinks = new List<Link>()
            {
                new Link
                {
                    Text = "Google",
                    Url = "https://google.com",
                },
                new Link
                {
                    Text = "Facebook",
                    Url = "https://facebook.com",
                },
            };
            var client = new WebApplicationFactory<Program>().CreateClient();

            var response = await client.GetAsync("/v1/users/1/links");
            var serialisedBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var actualLinks = JsonSerializer.Deserialize<IEnumerable<Link>>(serialisedBody, options);

            Assert.Equal(expectedLinks, actualLinks);
        }
    }
}