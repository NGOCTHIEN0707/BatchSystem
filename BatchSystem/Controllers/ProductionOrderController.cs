using BatchSystem.Application.Commands.ProductionOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BatchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionOrderController : ApiControllerBase
    {
        public ProductionOrderController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductionOrder([FromBody] CreateProductionOrderCommand command)
        {
            return await SendCommand(command);
        }

    }
}
