using InterviewProject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InterviewProject.Middlewares
{
    public class MetricsMiddleware
    {
        private readonly RequestDelegate _next;

        public MetricsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IMetricsService metrics)
        {
            try
            {
                await _next.Invoke(context);
                //Регистрируем успешный запрос
                metrics.IncreaseRequestCount(true);
            } catch
            {
                //Регистрируем запрос, вызвавший исключение
                metrics.IncreaseRequestCount(false);
            }
        }
    }
}
