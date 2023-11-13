using InterviewProject.Database.Models;
using System.Collections.Generic;

namespace InterviewProject.Database.DataSeeder
{
    public static class IntervieweeSeeder
    {
        public static IEnumerable<Interviewee> SeedData()
        {
            return new Interviewee[]
            {
                new Interviewee { Id = 1, FirstName = "Василий", LastName = "Васильев", MiddleName = "Васильевич" },
                new Interviewee { Id = 2, FirstName = "Сидор", LastName = "Сидоров", MiddleName = "Сидорович" },
                new Interviewee { Id = 3, FirstName = "James", LastName = "Bond", MiddleName = "" },
                new Interviewee { Id = 4, FirstName = "Doctor", LastName = "Watson" }
            };
        }
    }
}
