using BatchSystem.Application.Commands.Materials.Create;
using BatchSystem.Application.Commands.Materials.Deactivate;
using BatchSystem.Application.Commands.Materials.Update;
using BatchSystem.Application.Queries.Materials;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        [HttpGet("GetAll")]
        public async Task<List<MaterialDto>> GetAllMaterials([FromQuery] GetAllMaterialsQuery query )
        {
            return await _mediator.Send(query);
        }
    }
}
