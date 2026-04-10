using BatchSystem.Application.Commands.Stations.Create;
using BatchSystem.Application.Commands.Stations.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BatchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ApiControllerBase
    {
        public StationController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        public async Task<IActionResult> CreateStation([FromBody] CreateStationCommand command)
        {
            return await SendCommand(command);
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateStation([FromBody] UpdateStationCommand command)
        {
            return await SendCommand(command);
        }
    }
}
