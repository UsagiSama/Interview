using InterviewProject.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace InterviewProject.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                // теперь этот участок кода стал достижим
                // после фикса ошибки с передачей исключения в обратную сторону по конвейру
                // все тесты из InterviewerTests и большая часть тестов IntervieweeTests стали выполнятся положительно

                //Отлавливаем исключение и выставляем корректный код ответа
                context.Response.StatusCode = e switch
                {
                    BadRequestException => (int)HttpStatusCode.BadRequest,
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    ForbiddenException => (int)HttpStatusCode.Forbidden,
                    UnauthorizedException => (int)HttpStatusCode.Unauthorized,
                    _ => (int)HttpStatusCode.InternalServerError,
                };
            }
        }
    }
}
