using InterviewProject.Dtos;
using System.Collections.Generic;

namespace InterviewProject.Services.Interfaces
{
    public interface IInterviewService
    {
        IEnumerable<GetInterviewDto> GetInterviews();
        GetInterviewDto GetInterview(int id);
        void CreateInterview(CreateInterviewDto request);
        void UpdateInterview(UpdateInterviewDto request);
        void DeleteInterview(int id);
    }
}
