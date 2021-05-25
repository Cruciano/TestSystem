using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;

namespace IntegrationTests.ControllerTest
{
    [TestFixture]
    class ThemeControllerTest
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
            var response = await _client.GetAsync("/Theme");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("Theme_1");
            responseMessege.Should().Contain("Theme_2");
        }

        //Create
        [Test]
        public async Task Create_ReturnsCorrectPage()
        {
            var response = await _client.GetAsync("/Theme/Create");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("New theme");
        }


        [Test]
        public async Task Create_ReturnsInvalidInputPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Theme/Create");
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
            var request = new HttpRequestMessage(HttpMethod.Post, "/Theme/Create");
            var model = new Dictionary<string, string>
            {
                { "Title", "NewTheme" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();
            responseMessege.Should().Contain("NewTheme");
        }

        //Edit
        [Test]
        public async Task Edit_ReturnsCorrectPage()
        {
            var response = await _client.GetAsync("/Theme/Edit/1");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("Edit theme");
        }


        [Test]
        public async Task Edit_ReturnsInvalidInputPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Theme/Edit/1");
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
            var request = new HttpRequestMessage(HttpMethod.Post, "/Theme/Edit/1");
            var model = new Dictionary<string, string>
            {
                { "Title", "EditedTheme" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();
            responseMessege.Should().Contain("EditedTheme");
        }

        //Delete
        [Test]
        public async Task Delete_ReturnsCorrectPage()
        {
            var response = await _client.GetAsync("/Theme/Delete/1");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("Delete theme");
        }

        [Test]
        public async Task Delete_DeleteAndReturnsToIndexPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Theme/DeleteVerified/1");
            var model = new Dictionary<string, string>
            {
                { "Id", "1" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().NotContain("Theme_1");
        }
    }
}
