using InterviewProject.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewProject.Services.Interfaces
{
    public interface IIntervieweeService
    {
        Task<IEnumerable<GetIntervieweeDto>> GetInterviewees(CancellationToken token = default);
        Task<GetIntervieweeDto> GetInterviewee(int id, CancellationToken token = default);
        Task CreateInterviewee(CreateIntervieweeDto request, CancellationToken token = default);
        void UpdateInterviewee(UpdateIntervieweeDto request, CancellationToken token = default);
        void DeleteInterviewee(int id, CancellationToken token = default);
    }
}
