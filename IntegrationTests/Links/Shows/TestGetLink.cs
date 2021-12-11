using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using LinkPage.Links.Shows;
using System;
using LinkPage.Links.Classic;

namespace IntegrationTests.Links.Shows
{
    public class TestGetLink
    {
        private readonly List<ClassicLink> _testData = new()
        {
            new ShowsLink
            {
                LinkId = 1,
                UserId = 1,
                Title = "La Bohème",
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
            new ShowsLink
            {
                LinkId = 2,
                UserId = 1,
                Title = "Live at the Aquarium",
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
            new ShowsLink
            {
                LinkId = 1,
                UserId = 2,
                Title = "La Bohème",
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
            new ShowsLink
            {
                LinkId = 2,
                UserId = 2,
                Title = "Live at the Aquarium",
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
        };

        [Fact]
        public async Task WhenLinkExists_StatusCodeIsOK()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links/shows/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task WhenRequestHasUserId_BodyContainsLinkWithSameUserId(int userId)
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync($"/v1/users/{userId}/links/shows/1");
            var body = await response.Content.ReadAsStringAsync();
            var link = Helpers.Deserialize<ShowsLink>(body);

            Assert.Equal(userId, link?.UserId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task WhenRequestHasLinkId_BodyContainsLinkWithSameLinkId(int linkId)
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync($"/v1/users/1/links/shows/{linkId}");
            var body = await response.Content.ReadAsStringAsync();
            var link = Helpers.Deserialize<ShowsLink>(body);

            Assert.Equal(linkId, link?.LinkId);
        }

        [Fact]
        public async Task WhenLinkDoesNotExist_StatusCodeIsNotFound()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links/shows/3");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetLink_WhenLinkDoesNotExist_BodyIsEmpty()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links/shows/3");
            var serialisedBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(string.Empty, serialisedBody);
        }
    }
}