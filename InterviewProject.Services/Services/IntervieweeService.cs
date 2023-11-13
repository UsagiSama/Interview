using InterviewProject.Common.Exceptions;
using InterviewProject.Database.Context;
using InterviewProject.Database.Models;
using InterviewProject.Dtos;
using InterviewProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewProject.Services.Services
{
    public class IntervieweeService
        : IIntervieweeService
    {
        private InterviewDbContext _context;

        public IntervieweeService(InterviewDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetIntervieweeDto>> GetInterviewees(
            CancellationToken token = default)
        {
            var interviewees = await _context.Interviewees
                .ToListAsync(token);

            return interviewees
                .Select(x => new GetIntervieweeDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    MiddleName = x.MiddleName
                });
        }

        public async Task<GetIntervieweeDto> GetInterviewee(
            int id,
            CancellationToken token = default)
        {
            var interviewee = await _context.Interviewees
                .FirstOrDefaultAsync(x => x.Id == id, token)
                ?? throw new NotFoundException();

            return new GetIntervieweeDto
            {
                Id = interviewee.Id,
                FirstName = interviewee.FirstName,
                LastName = interviewee.LastName,
                MiddleName = interviewee.MiddleName
            };
        }

        public async Task CreateInterviewee(
            CreateIntervieweeDto request,
            CancellationToken token = default)
        {
            var interviewee = new Interviewee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName
            };

            _context.Add(interviewee);
            await _context.SaveChangesAsync(token);
        }

        public void UpdateInterviewee(
            UpdateIntervieweeDto request,
            CancellationToken token = default)
        {
            var interviewee = _context.Interviewees
                .FirstOrDefault(x => x.Id == request.Id)
                ?? throw new NotFoundException();

            interviewee.FirstName = request.FirstName;
            interviewee.LastName = request.LastName;
            interviewee.MiddleName = request.MiddleName;

            _context.SaveChangesAsync(token);
        }

        public void DeleteInterviewee(
            int id,
            CancellationToken token = default)
        {
            var interviewee = _context.Interviewees
                .FirstOrDefault(x => x.Id == id)
                ?? throw new NotFoundException();

            _context.Remove(interviewee);
            _context.SaveChangesAsync(token);
        }
    }
}
