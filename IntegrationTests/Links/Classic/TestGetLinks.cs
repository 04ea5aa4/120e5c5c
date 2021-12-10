using LinkPage.Links;
using LinkPage.Links.Classic;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Links.Classic
{
    public class TestGetLinks
    {
        private readonly List<Link> _testData = new()
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
        public async Task WhenLinksExist_StatusCodeIsOK()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task WhenLinksExist_ResponseContainsExpectedLinks()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links");
            var body = await response.Content.ReadAsStringAsync();
            var links = Helpers.Deserialize<IEnumerable<ClassicLink>>(body);

            var expectedLinks = _testData.Where(link => link.UserId == 1);
            Assert.Equal(expectedLinks, links);
        }

        [Fact]
        public async Task WhenLinksDoNotExist_StatusCodeIsNotFound()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/3/links");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task WhenLinksDoNotExist_ResponseContainsEmptyArray()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/3/links");
            var serialisedBody = await response.Content.ReadAsStringAsync();
            var actualLinks = Helpers.Deserialize<IEnumerable<ClassicLink>>(serialisedBody);

            Assert.Equal(Array.Empty<ClassicLink>(), actualLinks);
        }
    }
}