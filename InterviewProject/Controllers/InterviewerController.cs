using InterviewProject.Dtos;
using InterviewProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewProject.Controllers
{
    [Route("api/interviewers")]
    [ApiController]
    public class InterviewerController 
        : ControllerBase
    {
        private readonly IInterviewerService _service;

        public InterviewerController(IInterviewerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetInterviewerDto>>> GetInterview()
        {
            return Ok(await _service.GetInterviewers());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetInterviewerDto>> GetInterviews([FromRoute] int id)
        {
            return Ok(await _service.GetInterviewer(id));
        }

        [HttpPost]
        public ActionResult CreateInterview([FromBody] CreateInterviewerDto request)
        {
            _service.CreateInterviewer(request);
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateInterview([FromBody] UpdateInterviewerDto request)
        {
            _service.UpdateInterviewer(request);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteInterview([FromRoute] int id)
        {
            _service.DeleteInterviewer(id);
            return Ok();
        }
    }
}
