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
                        Id = id++,
                        Name = $"Interview_{id}",
                        IntervieweeId = intervieweeId,
                        InterviewerId = interviewerId
                    };
                }
            }
        }
    }
}
