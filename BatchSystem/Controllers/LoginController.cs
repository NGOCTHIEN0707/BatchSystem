using BatchSystem.Application.Commands.Logins.Authenticate;
using BatchSystem.Application.Commands.Logins.Create;
using BatchSystem.Application.Commands.Logins.Delete;
using BatchSystem.Application.Commands.Logins.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BatchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ApiControllerBase
    {
        public LoginController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLogin([FromBody] CreateLoginCommand command)
        {
            return await SendCommand(command);
        }
        [HttpPatch("update")]
        public async Task<IActionResult> Update([FromBody] UpdateLoginCommand command)
        {
            return await SendCommand(command);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteLogin([FromQuery] DeleteLoginCommand command)
        {
            return await SendCommand(command);
        }
        [HttpPost("AuthenticateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateCommand command)
        {
            return await SendCommand(command);
        }
    }
}
