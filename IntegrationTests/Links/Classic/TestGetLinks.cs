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
        private readonly TestData _testData = new TestData
        {
            ClassicLinks = new List<ClassicLinkModel>()
            {
                new ClassicLinkModel
                {
                    Id = 1,
                    Title = "Google",
                    Url = "https://google.com",
                },
                new ClassicLinkModel
                {
                    Id = 2,
                    Title = "Facebook",
                    Url = "https://facebook.com",
                },
            },
        };

        [Fact]
        public async Task GetLinks_WhenLinkExists_ReturnsStatusOK()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links/classic");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetLinks_WhenLinkExists_ContainsExpectedLinks()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(_testData);

            var response = await client.GetAsync("/v1/users/1/links/classic");
            var serialisedBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var actualLinks = JsonSerializer.Deserialize<IEnumerable<ClassicLinkModel>>(serialisedBody, options);

            Assert.Equal(_testData.ClassicLinks, actualLinks);
        }

        [Fact]
        public async Task GetLinks_WhenLinkDoesNotExist_ReturnsStatusNotFound()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(new TestData());

            var response = await client.GetAsync("/v1/users/1/links/classic");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetLinks_WhenLinkExists_ContainsEmptyArray()
        {
            var client = new WebApplicationFactory<Program>().CreateTestClient(new TestData());

            var response = await client.GetAsync("/v1/users/1/links/classic");
            var serialisedBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var actualLinks = JsonSerializer.Deserialize<IEnumerable<ClassicLinkModel>>(serialisedBody, options);

            Assert.Equal(Array.Empty<ClassicLinkModel>(), actualLinks);
        }
    }
}