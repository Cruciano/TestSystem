using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;

namespace IntegrationTests.ControllerTest
{
    [TestFixture]
    class AnswerControllerTest
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
            var response = await _client.GetAsync("/Answer?questionId=1");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("Answer_1");
            responseMessege.Should().NotContain("Answer_2");
        }

        //Create
        [Test]
        public async Task Create_ReturnsCorrectPage()
        {
            var response = await _client.GetAsync("/Answer/Create?questionId=1");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("New answer");
        }

        [Test]
        public async Task Create_ReturnsInvalidInputPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Answer/Create?questionId=1");
            var model = new Dictionary<string, string>
            {
                { "Text", "" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("The Text field is required");
        }

        [Test]
        public async Task Create_CreatesAndReturnsToIndexPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Answer/Create?questionId=1");
            var model = new Dictionary<string, string>
            {
                { "Text", "NewAnswer" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();
            responseMessege.Should().Contain("NewAnswer");
        }

        //Edit
        [Test]
        public async Task Edit_ReturnsCorrectPage()
        {
            var response = await _client.GetAsync("/Answer/Edit/1?questionId=1");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("Edit answer");
        }

        [Test]
        public async Task Edit_ReturnsInvalidInputPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Answer/Edit/1?questionId=1");
            var model = new Dictionary<string, string>
            {
                { "Text", "" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("The Text field is required");
        }

        [Test]
        public async Task Edit_EditAndReturnsToIndexPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Answer/Edit/1?questionId=1");
            var model = new Dictionary<string, string>
            {
                { "Text", "EditedAnswer" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();
            responseMessege.Should().Contain("EditedAnswer");
        }

        //Delete
        [Test]
        public async Task Delete_ReturnsCorrectPage()
        {
            var response = await _client.GetAsync("/Answer/Delete/1?questionId=1");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("Delete answer");
        }

        [Test]
        public async Task Delete_DeleteAndReturnsToIndexPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Answer/DeleteVerified/1");
            var model = new Dictionary<string, string>
            {
                { "Id", "1" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().NotContain("Answer_1");
        }

    }
}
