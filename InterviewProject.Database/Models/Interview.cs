namespace InterviewProject.Database.Models
{
    public class Interview
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int IntervieweeId { get; set; }
        public Interviewee Interviewee { get; set; }
        public int InterviewerId { get; set; }
        public Interviewer Interviewer { get; set; }
    }
}
