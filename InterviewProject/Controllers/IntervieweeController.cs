using InterviewProject.Dtos;
using InterviewProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewProject.Controllers
{
    [Route("api/interviewees")]
    [ApiController]
    public class IntervieweeController 
        : ControllerBase
    {
        private readonly IIntervieweeService _service;

        public IntervieweeController(IIntervieweeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetIntervieweeDto>>> GetInterviewees()
        {
            return Ok(await _service.GetInterviewees());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetIntervieweeDto>> GetInterviewee([FromRoute] int id)
        {
            return Ok(await _service.GetInterviewee(id));
        }

        [HttpPost]
        public async Task<ActionResult> CreateInterviewee([FromBody] CreateIntervieweeDto request)
        {
            await _service.CreateInterviewee(request);
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateInterview([FromBody] UpdateIntervieweeDto request)
        {
            _service.UpdateInterviewee(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteInterviewee([FromRoute] int id)
        {
            _service.DeleteInterviewee(id);
            return Ok();
        }
    }
}
