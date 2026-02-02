// SlaController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SLACalc.Application.SLA.Commands;
using SLACalc.Application.SLA.Queries;

namespace SlaCalculation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SlaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SlaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateSla([FromForm] CalculateSlaRequest request)
        {
            var command = new CalculateSlaCommand
            {
                Priority = request.Priority,
                CaptureDateTime = request.CaptureDateTime,
                Files = request.Files?.Select(f => new FileUpload
                {
                    FileName = f.FileName,
                    Content = f.OpenReadStream()
                }).ToList()
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("priorities")]
        public async Task<IActionResult> GetPriorities()
        {
            var query = new GetPrioritiesQuery();
            var priorities = await _mediator.Send(query);
            return Ok(priorities);
        }


    }

    public class CalculateSlaRequest
    {
        public string Priority { get; set; } = string.Empty;
        public DateTime CaptureDateTime { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
}