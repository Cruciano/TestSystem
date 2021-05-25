using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;

namespace IntegrationTests.ControllerTest
{
    [TestFixture]
    class ResultControllerTest
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
            var response = await _client.GetAsync("/Result");
            response.EnsureSuccessStatusCode();
            var responseMessege = await response.Content.ReadAsStringAsync();

            responseMessege.Should().Contain("Test_1");
        }
    }
}
