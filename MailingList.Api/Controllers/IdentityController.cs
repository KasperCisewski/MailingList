using MailingList.Api.Infrastructure.Options;
using MailingList.Api.Models.Requests.Identity;
using MailingList.Logic.Commands.Identity;
using MailingList.Logic.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MailingList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IMediator _mediator;

        public IdentityController(JwtOptions jwtOptions, IMediator mediator)
        {
            _jwtOptions = jwtOptions;
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(new RegisterCommand()
                {
                    Email = request.Email,
                    Username = request.Username,
                    Password = request.Password,
                    Secret = _jwtOptions.Secret
                }));
            }
            catch (LogicException ex)
            {
                return BadRequest(ex.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(new LoginCommand()
                {
                    Login = request.Login,
                    Password = request.Password,
                    Secret = _jwtOptions.Secret
                }));
            }
            catch (LogicException ex)
            {
                return BadRequest(ex.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
