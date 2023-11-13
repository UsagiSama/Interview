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
    public class InterviewTests
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
        public async Task GetInterviewsCountTest()
        {
            var json = await _client.GetAsync("api/interviews");
            var interviews = await json.Content.ReadFromJsonAsync<IEnumerable<GetInterviewDto>>();

            Assert.That(interviews.Count, Is.EqualTo(16));
        }

        [Test]
        [TestCase(1, "Interview_1", "Иванов И. И.", "Васильев В. В.")]
        [TestCase(2, "Interview_2", "Петров П. П.", "Васильев В. В.")]
        [TestCase(3, "Interview_3", "Smith J.", "Васильев В. В.")]
        [TestCase(4, "Interview_4", "Zimmer H.", "Васильев В. В.")]
        [TestCase(5, "Interview_5", "Иванов И. И.", "Сидоров С. С.")]
        [TestCase(6, "Interview_6", "Петров П. П.", "Сидоров С. С.")]
        [TestCase(7, "Interview_7", "Smith J.", "Сидоров С. С.")]
        [TestCase(8, "Interview_8", "Zimmer H.", "Сидоров С. С.")]
        [TestCase(9, "Interview_9", "Иванов И. И.", "Bond J.")]
        [TestCase(10, "Interview_10", "Петров П. П.", "Bond J.")]
        [TestCase(11, "Interview_11", "Smith J.", "Bond J.")]
        [TestCase(12, "Interview_12", "Zimmer H.", "Bond J.")]
        [TestCase(13, "Interview_13", "Иванов И. И.", "Watson D.")]
        [TestCase(14, "Interview_14", "Петров П. П.", "Watson D.")]
        [TestCase(15, "Interview_15", "Smith J.", "Watson D.")]
        [TestCase(16, "Interview_16", "Zimmer H.", "Watson D.")]
        public async Task GetInterviewsTest(
            int id,
            string name,
            string interviewer,
            string interviewee)
        {
            var json = await _client.GetAsync("api/interviews");
            var interviews = await json.Content.ReadFromJsonAsync<IEnumerable<GetInterviewDto>>();

            Assert.That(interviews.Any(x => x.Id == id 
                                    && x.Name == name 
                                    && x.Interviewer == interviewer 
                                    && x.Interviewee == interviewee), 
                        Is.True);
        }

        [Test]
        [TestCase(1, "Interview_1", "Иванов И. И.", "Васильев В. В.")]
        [TestCase(2, "Interview_2", "Петров П. П.", "Васильев В. В.")]
        [TestCase(3, "Interview_3", "Smith J.", "Васильев В. В.")]
        [TestCase(4, "Interview_4", "Zimmer H.", "Васильев В. В.")]
        [TestCase(5, "Interview_5", "Иванов И. И.", "Сидоров С. С.")]
        [TestCase(6, "Interview_6", "Петров П. П.", "Сидоров С. С.")]
        [TestCase(7, "Interview_7", "Smith J.", "Сидоров С. С.")]
        [TestCase(8, "Interview_8", "Zimmer H.", "Сидоров С. С.")]
        [TestCase(9, "Interview_9", "Иванов И. И.", "Bond J.")]
        [TestCase(10, "Interview_10", "Петров П. П.", "Bond J.")]
        [TestCase(11, "Interview_11", "Smith J.", "Bond J.")]
        [TestCase(12, "Interview_12", "Zimmer H.", "Bond J.")]
        [TestCase(13, "Interview_13", "Иванов И. И.", "Watson D.")]
        [TestCase(14, "Interview_14", "Петров П. П.", "Watson D.")]
        [TestCase(15, "Interview_15", "Smith J.", "Watson D.")]
        [TestCase(16, "Interview_16", "Zimmer H.", "Watson D.")]
        public async Task GetExistingInterview(
            int id,
            string name,
            string interviewer,
            string interviewee)
        {
            var json = await _client.GetAsync($"api/interviews/{id}");
            var interview = await json.Content.ReadFromJsonAsync<GetInterviewDto>();

            Assert.That(interview, Is.Not.Null);
            Assert.That(interview.Id == id, Is.True);
            Assert.That(interview.Name == name, Is.True);
            Assert.That(interview.Interviewee == interviewee, Is.True);
            Assert.That(interview.Interviewer == interviewer, Is.True);
        }

        [Test]
        [TestCase(0)]
        [TestCase(100500)]
        [TestCase(-1)]
        public async Task GetNonExistingInterview(int id)
        {
            var result = await _client.GetAsync($"api/interviews/{id}");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        [TestCase(1, 1, "Test_Interview_1")]
        [TestCase(1, 2, "Test_Interview_2")]
        [TestCase(1, 3, "Test_Interview_3")]
        public async Task CreateInterview(
            int intervieweeId,
            int interviewerId,
            string name
            )
        {
            var dto = new CreateInterviewDto
            {
                IntervieweeId = intervieweeId,
                InterviewerId = interviewerId,
                Name = name
            };

            var result = await _client.PostAsJsonAsync("api/interviews", dto);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [TestCase(16, 1, 1, "Test_Interview_1")]
        [TestCase(15, 1, 2, "Test_Interview_2")]
        [TestCase(14, 1, 3, "Test_Interview_3")]
        public async Task UpdateInterview(
            int id,
            int intervieweeId,
            int interviewerId,
            string name)
        {
            var dto = new UpdateInterviewDto
            {
                Id = id,
                IntervieweeId = intervieweeId,
                InterviewerId = interviewerId,
                Name = name
            };

            var result = await _client.PutAsJsonAsync("api/interviews", dto);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [TestCase(17, 1, 1, "Test_Interview_1")]
        [TestCase(18, 1, 2, "Test_Interview_2")]
        [TestCase(19, 1, 3, "Test_Interview_3")]
        public async Task UpdateNonExistingInterview(
            int id,
            int intervieweeId,
            int interviewerId,
            string name)
        {
            var dto = new UpdateInterviewDto
            {
                Id = id,
                IntervieweeId = intervieweeId,
                InterviewerId = interviewerId,
                Name = name
            };

            var result = await _client.PutAsJsonAsync("api/interviews", dto);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task DeleteInterview(int id)
        {
            var result = await _client.DeleteAsync($"api/interviews/{id}");

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        public async Task DeleteNonExistingInterview(int id)
        {
            var result = await _client.DeleteAsync($"api/interviews/{id}");

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
