﻿using MailingList.Api.Controllers.Base;
using MailingList.Api.Infrastructure.Options;
using MailingList.Api.Models;
using MailingList.Api.Models.Requests.Identity;
using MailingList.Logic.Commands.Identity;
using MailingList.Logic.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MailingList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
    public class IdentityController : BaseApiController<IdentityController>
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IMediator _mediator;

        public IdentityController(JwtOptions jwtOptions, IMediator mediator, ILogger<IdentityController> logger) : base(logger)
        {
            _jwtOptions = jwtOptions;
            _mediator = mediator;
        }

        /// <summary>
        /// Method used to user registration
        /// </summary>
        /// <returns>JWT authorization token</returns>
        [HttpPost("Register")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns authorization token", typeof(AuthorizationSuccessResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request", typeof(ApiErrorResultModel))]
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
            catch (Exception ex)
            {
                return ProcessErrorResponse(ex);
            }
        }

        /// <summary>
        /// Method used to user login into app
        /// </summary>
        /// <returns>JWT authorization token</returns>
        [HttpPost("Login")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns authorization token", typeof(AuthorizationSuccessResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request", typeof(ApiErrorResultModel))]
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
            catch (Exception ex)
            {
                return ProcessErrorResponse(ex);
            }
        }
    }
}
