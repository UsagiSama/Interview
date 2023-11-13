using InterviewProject.Database.Models;
using System.Collections.Generic;

namespace InterviewProject.Database.DataSeeder
{
    public static class InterviewSeeder
    {
        public static IEnumerable<Interview> SeedData()
        {
            int[] ids = new int[] { 1, 2, 3, 4 };
            int id = 1;

            foreach(var intervieweeId in ids)
            {
                foreach(var interviewerId in ids)
                {
                    yield return new Interview
                    {
                        Id = id,
                        Name = $"Interview_{id++}",
                        IntervieweeId = intervieweeId,
                        InterviewerId = interviewerId
                    };

                    // из-за того, что id инкрементировался на 19-й строке
                    // Name был некорректным
                    // После этого фикса 12 из 16-ти тестов GetInterviewsTest стали отрабатыть положительно
                }
            }
        }
    }
}
