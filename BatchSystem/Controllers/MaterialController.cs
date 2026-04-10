using BatchSystem.Application.Commands.Materials.Create;
using BatchSystem.Application.Commands.Materials.Deactivate;
using BatchSystem.Application.Commands.Materials.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BatchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ApiControllerBase
    {
        public MaterialController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateMaterial([FromBody] CreateMaterialCommand command)
        {
            return await SendCommand(command);
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateMaterial([FromBody] UpdateMaterialCommand command)
        {
            return await SendCommand(command);
        }
        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateMaterial([FromQuery] DeactivateMaterialCommand command)
        {
            return await SendCommand(command);
        }

    }
}
