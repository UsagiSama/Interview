using InterviewProject.Dtos;
using InterviewProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InterviewProject.Controllers
{
    [Route("api/interviews")]
    [ApiController]
    public class InterviewController
        : ControllerBase
    {
        private readonly IInterviewService interviewService;

        public InterviewController(IInterviewService service)
        {
            interviewService = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GetInterviewDto>> GetInterview()
        {
            return Ok(interviewService.GetInterviews());
        }

        [HttpGet("{id:int}")]
        public ActionResult<GetInterviewDto> GetInterviews([FromRoute]int id)
        {
            return Ok(interviewService.GetInterview(id));
        }

        [HttpPost]
        public ActionResult CreateInterview([FromBody]CreateInterviewDto request)
        {
            interviewService.CreateInterview(request);
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateInterview([FromBody]UpdateInterviewDto request)
        {
            interviewService.UpdateInterview(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteInterview([FromRoute]int id)
        {
            interviewService.DeleteInterview(id);
            return Ok();
        }
    }
}
