using InterviewProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterviewProject.Controllers
{
    [Route("api/metrics")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IMetricsService _service;

        public MetricsController(IMetricsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Produces("text/plain")]
        public ActionResult<string> GetMetrics()
        {
            return
@"
# HELP http_requests_total Total requests count
# TYPE http_requests_total counter
http_requests_total " + _service.RequestsCount.ToString() + @"

#HELP http_success_requests_total Total requests with 200 status
#TYPE http_success_requests_total counter
http_success_requests_total " + _service.SuccessCount.ToString() + @"

#HELP http_error_requests_total Total requests with non 200 status
#TYPE http_error_requests_total counter
http_error_requests_total " + _service.ErrorCount.ToString();
        }
    }
}
