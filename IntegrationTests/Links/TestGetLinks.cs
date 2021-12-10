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
        private readonly new List<Link> _testData = new()
        {
            new ClassicLink
            {
                LinkId = 1,
                UserId = 1,
                Title = "Google",
                Url = "https://google.com",
            },
            new ClassicLink
            {
                LinkId = 2,
                UserId = 1,
                Title = "Duck Duck Go",
                Url = "https://duckduckgo.com",
            },
            new ShowsLink
            {
                LinkId = 3,
                UserId = 1,
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

            var expectedBody = "[{\"url\":\"https://google.com\",\"linkId\":1,\"userId\":1,\"title\":\"Google\",\"linkType\":\"ClassicLink\"}," +
                "{\"url\":\"https://duckduckgo.com\",\"linkId\":2,\"userId\":1,\"title\":\"Duck Duck Go\",\"linkType\":\"ClassicLink\"}," +
                "{\"shows\":[{\"id\":1,\"date\":\"2022-01-01T00:00:00\",\"venueName\":\"Opera House\",\"venueLocation\":\"Sydney, Australia\",\"isSoldOut\":false,\"isOnSale\":true}],\"linkId\":3,\"userId\":1,\"title\":\"Shows\",\"linkType\":\"ShowsLink\"}]";
            Assert.Equal(expectedBody, serialisedBody);
        }

        [Fact]
        public async Task GetLinks_WhenLinkDoesNotExist_ReturnsStatusNotFound()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient();

            var response = await client.GetAsync("/v1/users/1/links");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetLinks_WhenLinkExists_ContainsEmptyArray()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient();

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