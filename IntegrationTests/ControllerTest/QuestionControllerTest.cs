using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;

namespace IntegrationTests.ControllerTest
{
    [TestFixture]
    class QuestionControllerTest
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
            var response = await _client.GetAsync("/Question?testId=1");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("Question_1");
            responseMessege.Should().NotContain("Question_2");
        }

        //Create
        [Test]
        public async Task Create_ReturnsCorrectPage()
        {
            var response = await _client.GetAsync("/Question/Create?testId=1");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("New question");
        }

        [Test]
        public async Task Create_ReturnsInvalidInputPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Question/Create?testId=1");
            var model = new Dictionary<string, string>
            {
                { "TaskText", "" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("The TaskText field is required");
        }

        [Test]
        public async Task Create_CreatesAndReturnsToIndexPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Question/Create?testId=1");
            var model = new Dictionary<string, string>
            {
                { "TaskText", "NewQuestion" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();
            responseMessege.Should().Contain("NewQuestion");
        }

        //Edit
        [Test]
        public async Task Edit_ReturnsCorrectPage()
        {
            var response = await _client.GetAsync("/Question/Edit/1?testId=1");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("Edit question");
        }

        [Test]
        public async Task Edit_ReturnsInvalidInputPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Question/Edit/1?testId=1");
            var model = new Dictionary<string, string>
            {
                { "TaskText", "" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("The TaskText field is required");
        }

        [Test]
        public async Task Edit_EditAndReturnsToIndexPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Question/Edit/1?testId=1");
            var model = new Dictionary<string, string>
            {
                { "TaskText", "EditedQuestion" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();
            responseMessege.Should().Contain("EditedQuestion");
        }

        //Delete
        [Test]
        public async Task Delete_ReturnsCorrectPage()
        {
            var response = await _client.GetAsync("/Question/Delete/1?testId=1");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("Delete question");
        }

        [Test]
        public async Task Delete_DeleteAndReturnsToIndexPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Question/DeleteVerified/1?testId=1");
            var model = new Dictionary<string, string>
            {
                { "Id", "1" }
            };
            request.Content = new FormUrlEncodedContent(model);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().NotContain("Question_1");
        }

    }
}
