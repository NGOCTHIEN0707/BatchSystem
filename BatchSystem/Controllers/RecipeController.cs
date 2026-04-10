using BatchSystem.Application.Commands.Recipes.Create;
using BatchSystem.Application.Commands.Recipes.Deactivate;
using BatchSystem.Application.Commands.Recipes.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BatchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ApiControllerBase
    {
        public RecipeController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeCommand command)
        {
            return await SendCommand(command);
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateRecipe([FromBody] UpdateRecipeCommand command)
        {
            return await SendCommand(command);
        }
        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateRecipe([FromQuery] DeactivateRecipeCommand command)
        {
            return await SendCommand(command);
        }
    }
}
