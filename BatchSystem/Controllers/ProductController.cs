using BatchSystem.Application.Commands.Products.Create;
using BatchSystem.Application.Commands.Products.DeactivateProduct;
using BatchSystem.Application.Commands.Prpoducts.Update;
using BatchSystem.Application.Queries.GetAllWithRecipe;
using BatchSystem.Application.Queries.Product_Recipe;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BatchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ApiControllerBase
    {
        public ProductController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            return await SendCommand(command);
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            return await SendCommand(command);
        }
        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateProduct([FromQuery] DeactivateProductCommand command)
        {
            return await SendCommand(command);
        }
        [HttpGet("GetAll")]
        public async Task<List<ProductWithRecipeDto>> GetProducts([FromQuery] GetAllProductsWithRecipeQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
