using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;
using LinkPage.Links;
using System.Collections.Generic;

namespace IntegrationTests.Links
{
    public class TestLinkIdMatching
    {

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetLink_WhenRequestHasLinkId_LinkHasId(int linkId)
        {
            var testData = new TestData
            {
                Links = new List<Link>
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
                },
            };
            var client = new WebApplicationFactory<Program>().CreateTestClient(testData);

            var response = await client.GetAsync($"/v1/users/1/links/{linkId}");
            var body = await response.Content.ReadAsStringAsync();
            var link = Helpers.Deserialize<ClassicLink>(body);

            Assert.Equal(linkId, link.LinkId);
        }
    }
}