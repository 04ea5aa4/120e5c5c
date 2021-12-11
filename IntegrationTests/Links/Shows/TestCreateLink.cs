using LinkPage.Links;
using LinkPage.Links.Classic;
using LinkPage.Links.Shows;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Links.Shows
{
    public class TestCreateLink
    {
        private readonly List<ClassicLink> _testData = new();

        [Fact]
        public async Task WhenLinkIsCreated_StatusCodeIsCreated()
        {
            var newLink = new ShowsLink
            {
                Title = "La Bohème",
                Url = "https://bookshows.com/iuh786BKHJ",
                Shows = new List<ShowsLink.Show>()
            };

            var response = await SendCreateRequest(newLink);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task WhenLinkIsCreated_LocationHeaderShowsLocationOfNewLink()
        {
            var newLink = new ShowsLink
            {
                Title = "La Bohème",
                Url = "https://bookshows.com/iuh786BKHJ",
                Shows = new List<ShowsLink.Show>()
            };

            var response = await SendCreateRequest(newLink);

            var createdLink = await ReadCreatedLink(response);
            var expectedLocation = $"v1/users/{createdLink?.UserId}/links/shows/{createdLink?.LinkId}";
            Assert.Equal(expectedLocation, response.Headers.Location?.ToString());
        }

        [Fact]
        public async Task WhenLinkIsCreated_LinkIdIsOverwritten()
        {
            var newLink = new ShowsLink
            {
                LinkId = 123,
                Title = "La Bohème",
                Url = "https://bookshows.com/iuh786BKHJ",
                Shows = new List<ShowsLink.Show>()
            };

            var response = await SendCreateRequest(newLink);

            var createdLink = await ReadCreatedLink(response);
            Assert.NotEqual(newLink.LinkId, createdLink?.LinkId);
        }

        [Fact]
        public async Task WhenLinkIsCreated_UserIdIsTakenFromUrl()
        {
            var newLink = new ShowsLink
            {
                UserId = 123,
                Title = "La Bohème",
                Url = "https://bookshows.com/iuh786BKHJ",
                Shows = new List<ShowsLink.Show>()
            };

            var response = await SendCreateRequest(newLink);

            var createdLink = await ReadCreatedLink(response);
            Assert.Equal(5678, createdLink?.UserId);
        }

        [Fact]
        public async Task WhenLinkIsCreated_CreatedLinkIsReturned()
        {
            var newLink = new ShowsLink
            {
                LinkId = 1,
                UserId = 5678,
                Title = "La Bohème",
                Url = "https://bookshows.com/iuh786BKHJ",
                Shows = new List<ShowsLink.Show>()
            };

            var response = await SendCreateRequest(newLink);

            var createdLink = await ReadCreatedLink(response);
            Assert.Equal(newLink, createdLink);
        }

        [Fact]
        public async Task WhenTitleIsMissing_BadRequestMessageIsReturned()
        {
            var newLink = new ShowsLink
            {
                Shows = new List<ShowsLink.Show>()
            };

            var response = await SendCreateRequest(newLink);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("'Title' must not be empty", content);
        }

        [Fact]
        public async Task WhenTitleIsMoreThan144Characters_BadRequestMessageIsReturned()
        {
            var newLink = new ShowsLink
            {
                Shows = new List<ShowsLink.Show>(),
                Title = "-------------100-chars------------------------------------------------------------------------------" +
                    "-----------------145-chars-------------------",
            };
            var response = await SendCreateRequest(newLink);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("The length of 'Title' must be 144 characters or fewer.", content);
        }

        [Fact]
        public async Task WhenShowIdsAreNotUnique_BadRequestMessageIsReturned()
        {
            var newLink = new ShowsLink
            {
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
            };
            var response = await SendCreateRequest(newLink);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Show IDs must be unique within a ShowsLink", content);
        }

        private async Task<HttpResponseMessage> SendCreateRequest(ShowsLink newLink)
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);
            var requestContent = new StringContent(Helpers.Serialize(newLink));
            requestContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            return await client.PostAsync("/v1/users/5678/links/shows", requestContent);
        }

        private static async Task<ShowsLink?> ReadCreatedLink(HttpResponseMessage responseMessage)
        {
            var body = await responseMessage.Content.ReadAsStringAsync();

            return Helpers.Deserialize<ShowsLink>(body);
        }
    }
}
