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
                // После этого фикса оставшиеся 4 теста GetInterviewsTest стали отрабатыть положительно
                yield return new GetInterviewDto
                {
                    Id = interview.Id,
                    Interviewee = CastNameResponseFormat(interview.Interviewee.FirstName, interview.Interviewee.MiddleName, interview.Interviewee.LastName),
                    Interviewer = CastNameResponseFormat(interview.Interviewer.FirstName, interview.Interviewer.MiddleName, interview.Interviewer.LastName),
                    Name = interview.Name
                };
            }
        }

        public GetInterviewDto GetInterview(int id)
        {
            // здесь не хватало данных о Interviewer и Interviewee
            // из-за чего происходило исключение при попытке обратится к ним
            // после этого фикса все тесты GetExistingInterview отрабатывают успешно
            var interview = _context.Interviews
                .Include(x => x.Interviewer)
                .Include(x => x.Interviewee)
                .FirstOrDefault(x => x.Id == id)
                ?? throw new NotFoundException();

            return new GetInterviewDto
            {
                Id = interview.Id,
                Interviewee = CastNameResponseFormat(interview.Interviewee.FirstName, interview.Interviewee.MiddleName, interview.Interviewee.LastName),
                Interviewer = CastNameResponseFormat(interview.Interviewer.FirstName, interview.Interviewer.MiddleName, interview.Interviewer.LastName),
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

        // DRY
        /// <summary>
        /// Формирует имя в отформатированном виде
        /// </summary>
        private string CastNameResponseFormat(string firstName, string middleName, string lastName)
        {
            string formattedName = lastName + " " + firstName[0] + ".";

            // если в MiddleName есть какое-то слово, то мы допишем инициал иначе - нет
            // из-за того, что MiddleName может быть null, сделал через if-ы
            if (!string.IsNullOrEmpty(middleName))
                if (char.IsLetter(middleName.FirstOrDefault()))
                    formattedName += " " + middleName[0] + ".";

            return formattedName;
        }
    }
}
