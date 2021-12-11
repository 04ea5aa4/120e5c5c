using LinkPage.Links;
using LinkPage.Links.Classic;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Links.Classic
{
    public class TestCreateLink
    {
        private readonly List<Link> _testData = new();

        [Fact]
        public async Task WhenLinkIsCreated_StatusCodeIsCreated()
        {
            var newLink = new ClassicLink
            {
                Title = "Radio New Zealand",
                Url = "https://rnz.co.nz",
            };

            var response = await SendCreateRequest(newLink);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task WhenLinkIsCreated_LocationHeaderShowsLocationOfNewLink()
        {
            var newLink = new ClassicLink
            {
                Title = "Radio New Zealand",
                Url = "https://rnz.co.nz",
            };

            var response = await SendCreateRequest(newLink);

            var createdLink = await ReadCreatedLink(response);
            var expectedLocation = $"v1/users/{createdLink?.UserId}/links/classic/{createdLink?.LinkId}";
            Assert.Equal(expectedLocation, response.Headers.Location?.ToString());
        }

        [Fact]
        public async Task WhenLinkIsCreated_LinkIdIsOverwritten()
        {
            var newLink = new ClassicLink
            {
                LinkId = 1234,
                Title = "Radio New Zealand",
                Url = "https://rnz.co.nz",
            };
            var response = await SendCreateRequest(newLink);

            var createdLink = await ReadCreatedLink(response);
            Assert.NotEqual(newLink.LinkId, createdLink?.LinkId);
        }

        [Fact]
        public async Task WhenLinkIsCreated_UserIdIsTakenFromUrl()
        {
            var newLink = new ClassicLink
            {
                UserId = 1234,
                Title = "Radio New Zealand",
                Url = "https://rnz.co.nz",
            };

            var response = await SendCreateRequest(newLink);

            var createdLink = await ReadCreatedLink(response);
            Assert.Equal(5678, createdLink?.UserId);
        }

        [Fact]
        public async Task WhenNewLinkUrlIsMissing_BadRequestMessageIsReturned()
        {
            var newLink = new ClassicLink
            {
                Title = "Radio New Zealand",
            };
            var response = await SendCreateRequest(newLink);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("'Url' must not be empty.", content);
        }

        [Fact]
        public async Task WhenNewLinkUrlIsMoreThan2084Characters_BadRequestMessageIsReturned()
        {
            var newLink = new ClassicLink
            {
                Title = "Radio New Zealand",
                Url =
                    "-----------------------------------------100-chars--------------------------------------------------" +
                    "-----------------------------------------200-chars--------------------------------------------------" +
                    "-----------------------------------------300-chars--------------------------------------------------" +
                    "-----------------------------------------400-chars--------------------------------------------------" +
                    "-----------------------------------------500-chars--------------------------------------------------" +
                    "-----------------------------------------600-chars--------------------------------------------------" +
                    "-----------------------------------------700-chars--------------------------------------------------" +
                    "-----------------------------------------800-chars--------------------------------------------------" +
                    "-----------------------------------------900-chars--------------------------------------------------" +
                    "-----------------------------------------1000-chars-------------------------------------------------" +
                    "-----------------------------------------1100-chars-------------------------------------------------" +
                    "-----------------------------------------1200-chars-------------------------------------------------" +
                    "-----------------------------------------1300-chars-------------------------------------------------" +
                    "-----------------------------------------1400-chars-------------------------------------------------" +
                    "-----------------------------------------1500-chars-------------------------------------------------" +
                    "-----------------------------------------1600-chars-------------------------------------------------" +
                    "-----------------------------------------1700-chars-------------------------------------------------" +
                    "-----------------------------------------1800-chars-------------------------------------------------" +
                    "-----------------------------------------1900-chars-------------------------------------------------" +
                    "-----------------------------------------2000-chars-------------------------------------------------" +
                    "-----------------------------------------2085-chars----------------------------------",
            };
            var response = await SendCreateRequest(newLink);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("The length of 'Url' must be 2084 characters or fewer.", content);
        }

        [Fact]
        public async Task WhenNewLinkTitleIsMissing_BadRequestMessageIsReturned()
        {
            var newLink = new ClassicLink
            {
                Url = "https://rnz.co.nz",
            };
            var response = await SendCreateRequest(newLink);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("'Title' must not be empty.", content);
        }

        [Fact]
        public async Task WhenNewLinkTitleIsMoreThan144Characters_BadRequestMessageIsReturned()
        {
            var newLink = new ClassicLink
            {
                Title = "-------------100-chars------------------------------------------------------------------------------" +
                    "-----------------145-chars-------------------",
                Url = "https://rnz.co.nz",
            };
            var response = await SendCreateRequest(newLink);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("The length of 'Title' must be 144 characters or fewer.", content);
        }

        private async Task<HttpResponseMessage> SendCreateRequest(ClassicLink newLink)
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);
            var requestContent = new StringContent(Helpers.Serialize(newLink));
            requestContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            return await client.PostAsync("/v1/users/5678/links/classic", requestContent);
        }

        private static async Task<ClassicLink?> ReadCreatedLink(HttpResponseMessage responseMessage)
        {
            var body = await responseMessage.Content.ReadAsStringAsync();

            return Helpers.Deserialize<ClassicLink>(body);
        }
    }
}
