using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using LinkPage.Links;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTests.Links.Classic
{
    public class TestGetLink
    {
        private readonly TestData _testData = new TestData
        {
            ClassicLinks = new List<ClassicLinkModel>()
            {
                new ClassicLinkModel
                {
                    Id = 1,
                    Title = "Google",
                    Url = "https://google.com",
                },
            },
        };

        [Fact]
        public async Task GetLink_WhenLinkExists_ReturnsStatusOK()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links/classic/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetLinks_WhenLinksExists_BodyContainsExpectedLinks()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links/classic/1");
            var serialisedBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var actualLink = JsonSerializer.Deserialize<ClassicLinkModel>(serialisedBody, options);

            Assert.Equal(_testData.ClassicLinks.First(), actualLink);
        }

        [Fact]
        public async Task GetLink_WhenLinkDoesNotExist_ReturnsStatusNotFound()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links/classic/2");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetLinks_WhenLinksExists_BodyIsEmpty()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links/classic/2");
            var serialisedBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(string.Empty, serialisedBody);
        }
    }
}