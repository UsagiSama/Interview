using InterviewProject.Common.Exceptions;
using InterviewProject.Database.Context;
using InterviewProject.Database.Models;
using InterviewProject.Dtos;
using InterviewProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewProject.Services.Services
{
    public class InterviewerService
        : IInterviewerService
    {
        private InterviewDbContext _context;

        public InterviewerService(InterviewDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetInterviewerDto>> GetInterviewers(
            CancellationToken token = default)
        {
            var interviewers = await _context.Interviewers
                .ToListAsync(token);

            return interviewers
                .Select(x => new GetInterviewerDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    MiddleName = x.MiddleName
                });
        }

        public async Task<GetInterviewerDto> GetInterviewer(int id)
        {
            var interviewer = await _context.Interviewers
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NotFoundException();

            return new GetInterviewerDto
            {
                Id = interviewer.Id,
                FirstName = interviewer.FirstName,
                LastName = interviewer.LastName,
                MiddleName = interviewer.MiddleName
            };
        }

        public void CreateInterviewer(CreateInterviewerDto request)
        {
            var interviewer = new Interviewer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName
            };

            _context.Add(interviewer);
            _context.SaveChanges();
        }

        public void UpdateInterviewer(UpdateInterviewerDto request)
        {
            var interviewer = _context.Interviewers
                .FirstOrDefault(x => x.Id == request.Id)
                ?? throw new NotFoundException();

            interviewer.FirstName = request.FirstName;
            interviewer.LastName = request.LastName;
            interviewer.MiddleName = request.MiddleName;

            _context.SaveChanges();
        }

        public void DeleteInterviewer(int id)
        {
            var interviewer = _context.Interviewers
                .FirstOrDefault(x => x.Id == id)
                ?? throw new NotFoundException();

            _context.Remove(interviewer);
            _context.SaveChanges();
        }
    }
}
