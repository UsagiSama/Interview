using InterviewProject.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewProject.Services.Interfaces
{
    public interface IInterviewerService
    {
        Task<IEnumerable<GetInterviewerDto>> GetInterviewers(CancellationToken token = default);
        Task<GetInterviewerDto> GetInterviewer(int id);
        void CreateInterviewer(CreateInterviewerDto request);
        void UpdateInterviewer(UpdateInterviewerDto request);
        void DeleteInterviewer(int id);
    }
}
