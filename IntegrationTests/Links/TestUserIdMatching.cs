using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;
using LinkPage.Links;
using System.Collections.Generic;

namespace IntegrationTests.Links
{
    public class TestUserIdMatching
    {
        private readonly TestData _testData = new()
        {
            Links = new List<Link>()
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
            },
        };

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetLink_WhenRequestHasUserId_LinkHasUserId(int userId)
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync($"/v1/users/{userId}/links/1");
            var body = await response.Content.ReadAsStringAsync();
            var link = Helpers.Deserialize<ClassicLink>(body);

            Assert.Equal(userId, link.UserId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetLinks_WhenRequestHasUserId_LinksHaveUserId(int userId)
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync($"/v1/users/{userId}/links");
            var body = await response.Content.ReadAsStringAsync();
            var links = Helpers.Deserialize<IEnumerable<ClassicLink>>(body);

            Assert.All(links, (link) => link.UserId = userId);
        }
    }
}