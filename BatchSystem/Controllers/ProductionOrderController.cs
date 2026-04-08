using BatchSystem.Application.Commands.ProductionOrders.ChangeStatus;
using BatchSystem.Application.Commands.ProductionOrders.Create;
using BatchSystem.Application.Commands.ProductionOrders.Update;
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
        [HttpPatch]
        public async Task<IActionResult> UpdateProductionOrder([FromBody] UpdateProductionOrderCommand command)
        {
            return await SendCommand(command);
        }
        [HttpPatch("ConfirmReady")]
        public async Task<IActionResult> ConfirmReady([FromBody] ChangeProductionOrderStatusToReadyCommand command)
        {
            return await SendCommand(command);
        }
    }
}
