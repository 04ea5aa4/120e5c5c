using LinkPage;
using LinkPage.Links;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Links.Classic
{
    public class TestGetLinks
    {
        private readonly TestData _testData = new()
        {
            Links = new List<Link>()
            {
                new ClassicLink
                {
                    Title = "Google",
                    Url = "https://google.com",
                },
                new ClassicLink
                {
                    Title = "Duck Duck Go",
                    Url = "https://duckduckgo.com",
                },
                new ShowsLink
                {
                    Title = "Shows",
                    Shows = new List<ShowsLink.Show>
                    {
                        new ShowsLink.Show
                        {
                            Id = 1,
                            Date =  new DateTime(2022, 1, 1),
                            VenueName = "Opera House",
                            VenueLocation = "Sydney, Australia",
                            IsSoldOut = false,
                            IsOnSale = true,
                        },
                    },
                },
            },
        };

        [Fact]
        public async Task GetLinks_WhenLinkExists_ReturnsStatusOK()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetLinks_WhenLinkExists_ContainsExpectedLinks()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links");
            var serialisedBody = await response.Content.ReadAsStringAsync();

            var expectedBody = "[{\"url\":\"https://google.com\",\"id\":0,\"title\":\"Google\",\"linkType\":\"ClassicLink\"}," +
                "{\"url\":\"https://duckduckgo.com\",\"id\":0,\"title\":\"Duck Duck Go\",\"linkType\":\"ClassicLink\"}," +
                "{\"shows\":[{\"id\":1,\"date\":\"2022-01-01T00:00:00\",\"venueName\":\"Opera House\",\"venueLocation\":\"Sydney, Australia\",\"isSoldOut\":false,\"isOnSale\":true}],\"id\":0,\"title\":\"Shows\",\"linkType\":\"ShowsLink\"}]";
            Assert.Equal(expectedBody, serialisedBody);
        }

        [Fact]
        public async Task GetLinks_WhenLinkDoesNotExist_ReturnsStatusNotFound()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(new TestData());

            var response = await client.GetAsync("/v1/users/1/links");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetLinks_WhenLinkExists_ContainsEmptyArray()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(new TestData());

            var response = await client.GetAsync("/v1/users/1/links");
            var serialisedBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var actualLinks = JsonSerializer.Deserialize<IEnumerable<ClassicLink>>(serialisedBody, options);

            Assert.Equal(Array.Empty<ClassicLink>(), actualLinks);
        }
    }
}