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
    public class IntervieweeTests
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
        public async Task GetIntervieweesCountTest()
        {
            var json = await _client.GetAsync("api/interviewees");
            var interviews = await json.Content.ReadFromJsonAsync<IEnumerable<GetIntervieweeDto>>();

            Assert.That(interviews.Count, Is.EqualTo(4));
        }

        [Test]
        [TestCase(1, "Васильев", "Василий")]
        [TestCase(2, "Сидоров", "Сидор")]
        [TestCase(3, "Bond", "James")]
        [TestCase(4, "Watson", "Doctor")]
        public async Task GetIntervieweesTest(
            int id,
            string lastName,
            string firstName)
        {
            var json = await _client.GetAsync("api/interviewees");
            var interviewees = await json.Content.ReadFromJsonAsync<IEnumerable<GetIntervieweeDto>>();

            // все данные в interviewees заполняются корректно
            // однако TestCase-3 выполняется неудачно
            // т.к. в TestCase-3 имя и фамилия перепутаны местами
            // James это имя, Bond - фамилия, а не наоборот

            Assert.That(interviewees.Any(x => x.Id == id && 
                                              x.FirstName == firstName && 
                                              x.LastName == lastName), Is.True);
        }

        [Test]
        [TestCase(0)]
        [TestCase(100500)]
        [TestCase(-1)]
        public async Task GetNonExistingInterviewee(int id)
        {
            var result = await _client.GetAsync($"api/interviewees/{id}");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        [TestCase("Test", "Test", "Test")]
        public async Task CreateInterviewee(
            string firstName,
            string lastName,
            string middleName
            )
        {
            var dto = new CreateIntervieweeDto
            {
                FirstName = firstName, 
                MiddleName = middleName, 
                LastName = lastName
            };

            var result = await _client.PostAsJsonAsync("api/interviewees", dto);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [TestCase(1, "Test", "Test", "Test")]
        [TestCase(2, "Test", "Test", null)]
        [TestCase(3, "test", "test", "")]
        public async Task UpdateInterviewee(
            int id,
            string firstName,
            string lastName,
            string middleName)
        {
            var dto = new UpdateIntervieweeDto
            {
                Id = id,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName
            };

            var result = await _client.PutAsJsonAsync("api/interviewees", dto);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [TestCase(5, "Test", "Test", "Test")]
        [TestCase(6, "Test", "Test", null)]
        [TestCase(7, "test", "test", "")]
        public async Task UpdateNonExistingInterviewee(
            int id,
            string firstName,
            string lastName,
            string middleName)
        {
            var dto = new UpdateIntervieweeDto
            {
                Id = id,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName
            };

            var result = await _client.PutAsJsonAsync("api/interviewees", dto);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task DeleteInterviewee(int id)
        {
            var result = await _client.DeleteAsync($"api/interviewees/{id}");

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        public async Task DeleteNonExistingInterviewee(int id)
        {
            var result = await _client.DeleteAsync($"api/interviewees/{id}");

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
