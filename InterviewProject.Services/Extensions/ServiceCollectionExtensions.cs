using InterviewProject.Services.Interfaces;
using InterviewProject.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewProject.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInterviewServices(this IServiceCollection services)
        {
            services.AddScoped<IIntervieweeService, IntervieweeService>();
            services.AddScoped<IInterviewService, InterviewService>();
            services.AddScoped<IInterviewerService, InterviewerService>();
            services.AddSingleton<IMetricsService, MetricsService>();

            return services;
        }
    }
}
