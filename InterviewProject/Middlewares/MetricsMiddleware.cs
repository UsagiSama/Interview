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
            } 
            catch
            {
                // из endpoint-а DeleteInterviewee отлавливаем исключение
                // до исправлений здесь распространение исключений из endpoint-ов заканчивалось

                //Регистрируем запрос, вызвавший исключение
                metrics.IncreaseRequestCount(false);

                // т.к. в конвейере существует специальный middleware для маппинга классов исключений с кодами ошибок,
                // который находится выше по конвееру
                // распростроняем исключение в обратную сторону по конвейеру
                throw;
            }
        }
    }
}
