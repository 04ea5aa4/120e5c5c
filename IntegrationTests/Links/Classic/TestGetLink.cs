using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using LinkPage.Links;
using System.Collections.Generic;
using LinkPage.Links.Classic;

namespace IntegrationTests.Links.Classic
{
    public class TestGetLink
    {
        private readonly List<ClassicLink> _testData = new()
        {
            new ClassicLink
            {
                LinkId = 1,
                UserId = 1,
                Title = "DuckDuckGo",
                Url = "https://duckduckgo.com",
            },
            new ClassicLink
            {
                LinkId = 2,
                UserId = 1,
                Title = "Signal",
                Url = "https://signal.org",
            },
            new ClassicLink
            {
                LinkId = 1,
                UserId = 2,
                Title = "Google",
                Url = "https://google.com",
            },
            new ClassicLink
            {
                LinkId = 2,
                UserId = 2,
                Title = "Messenger",
                Url = "https://www.messenger.com",
            },
        };

        [Fact]
        public async Task WhenLinkExists_StatusCodeIsOK()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links/classic/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task WhenRequestHasUserId_BodyContainsLinkWithSameUserId(int userId)
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync($"/v1/users/{userId}/links/classic/1");
            var body = await response.Content.ReadAsStringAsync();
            var link = Helpers.Deserialize<ClassicLink>(body);

            Assert.Equal(userId, link?.UserId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task WhenRequestHasLinkId_BodyContainsLinkWithSameLinkId(int linkId)
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync($"/v1/users/1/links/classic/{linkId}");
            var body = await response.Content.ReadAsStringAsync();
            var link = Helpers.Deserialize<ClassicLink>(body);

            Assert.Equal(linkId, link?.LinkId);
        }

        [Fact]
        public async Task WhenLinkDoesNotExist_StatusCodeIsNotFound()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links/classic/3");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetLink_WhenLinkDoesNotExist_BodyIsEmpty()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links/classic/3");
            var serialisedBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(string.Empty, serialisedBody);
        }
    }
}