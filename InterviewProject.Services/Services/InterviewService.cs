using InterviewProject.Common.Exceptions;
using InterviewProject.Database.Context;
using InterviewProject.Database.Models;
using InterviewProject.Dtos;
using InterviewProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProject.Services.Services
{
    public class InterviewService
        : IInterviewService
    {
        private InterviewDbContext _context;

        public InterviewService(InterviewDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GetInterviewDto> GetInterviews()
        {
            foreach (var interview in _context.Interviews
                .Include(x => x.Interviewer)
                .Include(x => x.Interviewee))
            {
                yield return new GetInterviewDto
                {
                    Id = interview.Id,
                    Interviewee = interview.Interviewee.LastName + " " + interview.Interviewee.FirstName[0]
                        + "." + (!string.IsNullOrEmpty(interview.Interviewee.MiddleName)
                            ? " " + interview.Interviewee.MiddleName[0] + "."
                            : ""),
                    Interviewer = interview.Interviewer.LastName + " " + interview.Interviewer.FirstName[0]
                        + "." + (!string.IsNullOrEmpty(interview.Interviewer.MiddleName)
                            ? " " + interview.Interviewer.MiddleName[0] + "."
                            : ""),
                    Name = interview.Name
                };
            }
        }

        public GetInterviewDto GetInterview(int id)
        {
            var interview = _context.Interviews
                .FirstOrDefault(x => x.Id == id)
                ?? throw new NotFoundException();

            return new GetInterviewDto
            {
                Id = interview.Id,
                Interviewee = interview.Interviewee.LastName + " " + interview.Interviewee.FirstName[0]
                        + "." + (!string.IsNullOrEmpty(interview.Interviewee.MiddleName)
                            ? " " + interview.Interviewee.MiddleName[0] + "."
                            : ""),
                Interviewer = interview.Interviewer.LastName + " " + interview.Interviewer.FirstName[0]
                        + "." + (!string.IsNullOrEmpty(interview.Interviewer.MiddleName)
                            ? " " + interview.Interviewer.MiddleName[0] + "."
                            : ""),
                Name = interview.Name
            };
        }

        public void CreateInterview(CreateInterviewDto request)
        {
            var interview = new Interview
            {
                IntervieweeId = request.IntervieweeId,
                InterviewerId = request.InterviewerId,
                Name = request.Name
            };

            _context.Add(interview);
            _context.SaveChanges();
        }

        public void UpdateInterview(UpdateInterviewDto request)
        {
            var interview = _context.Interviews
                .FirstOrDefault(x => x.Id == request.Id)
                ?? throw new NotFoundException();

            interview.Name = request.Name;
            interview.IntervieweeId = request.IntervieweeId;
            interview.InterviewerId = request.InterviewerId;

            _context.SaveChanges();
        }

        public void DeleteInterview(int id)
        {
            var interview = _context.Interviews
                .FirstOrDefault(x => x.Id == id)
                ?? throw new NotFoundException();

            _context.Remove(interview);
            _context.SaveChanges();
        }
    }
}
