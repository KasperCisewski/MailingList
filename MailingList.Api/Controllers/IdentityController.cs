using MailingList.Api.Examples.Identity;
using MailingList.Api.Infrastructure.Options;
using MailingList.Api.Models.Requests.Identity;
using MailingList.Logic.Commands.Identity;
using MailingList.Logic.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MailingList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
    public class IdentityController : ControllerBase
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IMediator _mediator;
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(JwtOptions jwtOptions, IMediator mediator, ILogger<IdentityController> logger)
        {
            _jwtOptions = jwtOptions;
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("Register")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns token")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(SuccessfullLoginAndRegistrationExample))]
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
                _logger.LogError(ex.ToString());
                return BadRequest(new Dictionary<LogicErrorCode, string>() { { ex.ErrorCode, ex.Message } });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns token")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(SuccessfullLoginAndRegistrationExample))]
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
                _logger.LogError(ex.ToString());
                return BadRequest(new Dictionary<LogicErrorCode, string>() { { ex.ErrorCode, ex.Message } });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
