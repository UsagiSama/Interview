using InterviewProject.Services.Interfaces;

namespace InterviewProject.Services.Services
{
    public class MetricsService
        : IMetricsService
    {
        public int RequestsCount { get; set; }

        public int ErrorCount { get; set; }

        public int SuccessCount { get; set; }

        public MetricsService()
        {
            RequestsCount = 0;
            ErrorCount = 0;
            SuccessCount = 0;
        }

        public void IncreaseRequestCount(bool isSuccess)
        {
            RequestsCount++;

            ErrorCount += isSuccess ? 0 : 1;
            SuccessCount += isSuccess ? 1 : 0;
        }
    }
}
