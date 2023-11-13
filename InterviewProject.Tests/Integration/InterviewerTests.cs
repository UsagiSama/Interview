using InterviewProject.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace InterviewProject.Tests.Integration
{
    [TestFixture]
    public class InterviewerTests
    {
        private TestServer _server;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _server = new TestServer(new WebHostBuilder()
                    .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _server.Dispose();
        }

        [Test]
        public async Task GetInterviewersCountTest()
        {
            var json = await _client.GetAsync("api/interviewers");
            var interviews = await json.Content.ReadFromJsonAsync<IEnumerable<GetInterviewerDto>>();

            Assert.That(interviews.Count, Is.EqualTo(4));
        }

        [Test]
        [TestCase(1, "Иванов", "Иван")]
        [TestCase(2, "Петров", "Петр")]
        [TestCase(3, "Smith", "John")]
        [TestCase(4, "Zimmer", "Hans")]
        public async Task GetInterviewersTest(
            int id,
            string lastName,
            string firstName)
        {
            var json = await _client.GetAsync("api/interviewers");
            var interviewers = await json.Content.ReadFromJsonAsync<IEnumerable<GetInterviewerDto>>();

            Assert.That(interviewers.Any(x => x.Id == id 
                                    && x.FirstName == firstName
                                    && x.LastName == lastName), 
                        Is.True);
        }

        [Test]
        [TestCase(0)]
        [TestCase(100500)]
        [TestCase(-1)]
        public async Task GetNonExistingInterviewer(int id)
        {
            var result = await _client.GetAsync($"api/interviewers/{id}");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        [TestCase("Test", "Test", "Test")]
        public async Task CreateInterviewer(
            string firstName,
            string lastName,
            string middleName
            )
        {
            var dto = new CreateInterviewerDto
            {
                FirstName = firstName, 
                MiddleName = middleName, 
                LastName = lastName
            };

            var result = await _client.PostAsJsonAsync("api/interviewers", dto);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [TestCase(1, "Test", "Test", "Test")]
        [TestCase(2, "Test", "Test", null)]
        [TestCase(3, "test", "test", "")]
        public async Task UpdateInterviewer(
            int id,
            string firstName,
            string lastName,
            string middleName)
        {
            var dto = new UpdateInterviewerDto
            {
                Id = id,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName
            };

            var result = await _client.PutAsJsonAsync("api/interviewers", dto);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [TestCase(5, "Test", "Test", "Test")]
        [TestCase(6, "Test", "Test", null)]
        [TestCase(7, "test", "test", "")]
        public async Task UpdateNonExistingInterviewer(
            int id,
            string firstName,
            string lastName,
            string middleName)
        {
            var dto = new UpdateInterviewerDto
            {
                Id = id,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName
            };

            var result = await _client.PutAsJsonAsync("api/interviewers", dto);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task DeleteInterviewer(int id)
        {
            var result = await _client.DeleteAsync($"api/interviewers/{id}");

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        public async Task DeleteNonExistingInterviewer(int id)
        {
            var result = await _client.DeleteAsync($"api/interviewers/{id}");

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
