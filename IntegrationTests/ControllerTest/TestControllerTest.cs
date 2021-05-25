using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;

namespace IntegrationTests.ControllerTest
{
    [TestFixture]
    class TestControllerTest
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            var factory = new WebAppFactory();
            _client = factory.CreateClient();
        }

        [Test]
        public async Task Index_ReturnsCorrectPage()
        {
            var response = await _client.GetAsync("/Test?themeId=1");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("Test_1");
            responseMessege.Should().NotContain("Test_2");
        }

        //Create
        [Test]
        public async Task Create_ReturnsCorrectPage()
        {
            var response = await _client.GetAsync("/Test/Create?themeId=1");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("New test");
        }

        [Test]
        public async Task Create_ReturnsInvalidInputPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Test/Create?themeId=1");
            var model = new Dictionary<string, string>
            {
                { "Title", "" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("The Title field is required");
        }

        [Test]
        public async Task Create_CreatesAndReturnsToIndexPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Test/Create?themeId=1");
            var model = new Dictionary<string, string>
            {
                { "Title", "NewTest" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();
            responseMessege.Should().Contain("NewTest");
        }

        //Edit
        [Test]
        public async Task Edit_ReturnsCorrectPage()
        {
            var response = await _client.GetAsync("/Test/Edit/1?themeId=1");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("Edit test");
        }

        [Test]
        public async Task Edit_ReturnsInvalidInputPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Test/Edit/1?themeId=1");
            var model = new Dictionary<string, string>
            {
                { "Title", "" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("The Title field is required");
        }

        [Test]
        public async Task Edit_EditAndReturnsToIndexPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Test/Edit/1?themeId=1");
            var model = new Dictionary<string, string>
            {
                { "Title", "EditedTest" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();
            responseMessege.Should().Contain("EditedTest");
        }

        //Delete
        [Test]
        public async Task Delete_ReturnsCorrectPage()
        {
            var response = await _client.GetAsync("/Test/Delete/1?themeId=1");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("Delete test");
        }

        [Test]
        public async Task Delete_DeleteAndReturnsToIndexPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Test/DeleteVerified/1?themeId=1");
            var model = new Dictionary<string, string>
            {
                { "Id", "1" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().NotContain("Test_1");
        }
    }
}
