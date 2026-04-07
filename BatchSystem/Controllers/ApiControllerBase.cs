using BatchSystem.Application.Exceptions;
using BatchSystem.Application.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BatchSystem.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;

        public ApiControllerBase(IMediator mediator)
        {
            _mediator=mediator;
        }
        protected async Task<IActionResult> SendCommand<T>(IRequest<T> request)
        {
            try
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ErrorMessage(ex));
            }
            catch (EntityDuplicationException ex)
            {
                return Conflict(new ErrorMessage(ex));
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(new ErrorMessage(ex));
            }
        }
    }
}
