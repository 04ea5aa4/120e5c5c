using LinkPage.Links;
using LinkPage.Links.Classic;
using LinkPage.Links.Shows;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Links.Shows
{
    public class TestGetLinks
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
        public async Task WhenLinksExist_StatusCodeIsOK()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links/shows");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task WhenLinksExist_ResponseContainsExpectedLinks()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links/shows");
            var body = await response.Content.ReadAsStringAsync();
            var links = Helpers.Deserialize<IEnumerable<ShowsLink>>(body);

            var expectedLinks = _testData.Where(link => link.UserId == 1);
            Assert.Equal(expectedLinks, links);
        }

        [Fact]
        public async Task WhenLinksDoNotExist_StatusCodeIsNotFound()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/3/links/shows");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task WhenLinksDoNotExist_ResponseContainsEmptyArray()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/3/links/shows");
            var serialisedBody = await response.Content.ReadAsStringAsync();
            var actualLinks = Helpers.Deserialize<IEnumerable<ShowsLink>>(serialisedBody);

            Assert.Equal(Array.Empty<ShowsLink>(), actualLinks);
        }
    }
}