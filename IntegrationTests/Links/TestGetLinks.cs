using LinkPage.Links;
using LinkPage.Links.Classic;
using LinkPage.Links.Shows;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Links
{
    public class TestGetLinks
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
            new ShowsLink
            {
                LinkId = 2,
                UserId = 1,
                Title = "Live at the Aquarium",
                Url = "https://bookshows.com/latqsdhfljksdh",
                Shows = new List<ShowsLink.Show>
                {
                    new ShowsLink.Show
                    {
                        Id = 1,
                        Date = new DateTime(2022, 1, 1),
                        VenueName = "The Aquarium of Western Australia",
                        VenueLocation = "Perth, Australia",
                        IsSoldOut = false,
                        IsOnSale = true,
                    },
                },
            },
            new ClassicLink
            {
                LinkId = 1,
                UserId = 2,
                Title = "Signal",
                Url = "https://signal.org",
            },
            new ShowsLink
            {
                LinkId = 2,
                UserId = 2,
                Title = "La Boh�me",
                Url = "https://bookshows.com/djkHIUY987ydsf",
                Shows = new List<ShowsLink.Show>
                {
                    new ShowsLink.Show
                    {
                        Id = 1,
                        Date = new DateTime(2022, 1, 4),
                        VenueName = "Sydney Opera House",
                        VenueLocation = "Sydney, Australia",
                        IsSoldOut = false,
                        IsOnSale = true,
                    },
                },
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
        public async Task WhenRequestisForUserOne_ResponseContainsLinksForUserOne()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links");
            var actualBody = await response.Content.ReadAsStringAsync();
            var expectedBody = "[" +
                "{\"linkId\":1,\"userId\":1,\"title\":\"DuckDuckGo\",\"url\":\"https://duckduckgo.com\",\"linkType\":\"ClassicLink\"}," +
                "{\"shows\":[{\"id\":1,\"date\":\"2022-01-01T00:00:00\",\"venueName\":\"The Aquarium of Western Australia\",\"venueLocation\":\"Perth, Australia\",\"isSoldOut\":false,\"isOnSale\":true}],\"linkId\":2,\"userId\":1,\"title\":\"Live at the Aquarium\",\"url\":\"https://bookshows.com/latqsdhfljksdh\",\"linkType\":\"ShowsLink\"}" +
                "]";
            Assert.Equal(expectedBody, actualBody);
        }

        [Fact]
        public async Task WhenRequestisForUserTwo_ResponseContainsLinksForUserTwo()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/2/links");
            var actualBody = await response.Content.ReadAsStringAsync();

            var expectedBody = "[" +
                "{\"linkId\":1,\"userId\":2,\"title\":\"Signal\",\"url\":\"https://signal.org\",\"linkType\":\"ClassicLink\"}," +
                "{\"shows\":[{\"id\":1,\"date\":\"2022-01-04T00:00:00\",\"venueName\":\"Sydney Opera House\",\"venueLocation\":\"Sydney, Australia\",\"isSoldOut\":false,\"isOnSale\":true}],\"linkId\":2,\"userId\":2,\"title\":\"La Boh�me\",\"url\":\"https://bookshows.com/djkHIUY987ydsf\",\"linkType\":\"ShowsLink\"}" +
                "]";
            Assert.Equal(expectedBody, actualBody);
        }

        [Fact]
        public async Task WhenRequestisForUserThree_StatusCodeIsNotFound()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/3/links/shows");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task WhenRequestisForUserThree_ResponseContainsEmptyArray()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/3/links/shows");
            var serialisedBody = await response.Content.ReadAsStringAsync();
            var actualLinks = Helpers.Deserialize<IEnumerable<ShowsLink>>(serialisedBody);

            Assert.Equal(Array.Empty<ShowsLink>(), actualLinks);
        }
    }
}