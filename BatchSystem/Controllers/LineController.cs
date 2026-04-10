using BatchSystem.Application.Commands.Lines.Create;
using BatchSystem.Application.Commands.Lines.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BatchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineController : ApiControllerBase
    {
        public LineController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        public async Task<IActionResult> CreateLine([FromBody] CreateLineCommand command)
        {
            return await SendCommand(command);  
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateLine([FromBody] UpdateLineCommand command)
        {
            return await SendCommand(command);
        }
        
    }
}
