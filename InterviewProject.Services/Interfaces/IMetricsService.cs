namespace InterviewProject.Services.Interfaces
{
    public interface IMetricsService
    {
        public int RequestsCount { get; }
        public int ErrorCount { get; }
        public int SuccessCount { get; }

        public void IncreaseRequestCount(bool isSuccess);
    }
}
