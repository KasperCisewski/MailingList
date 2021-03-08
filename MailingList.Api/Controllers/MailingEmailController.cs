using MailingList.Api.Controllers.Base;
using MailingList.Api.Infrastructure.Extensions;
using MailingList.Api.Models;
using MailingList.Api.Models.Requests.MailingGroupEmail;
using MailingList.Logic.Commands.MailingEmail;
using MailingList.Logic.Models.Responses;
using MailingList.Logic.Queries.MailingEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MailingList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailingEmailController : BaseApiController<MailingEmailController>
    {
        private readonly IMediator _mediator;

        public MailingEmailController(ILogger<MailingEmailController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Method used to get mailing emails for user
        /// </summary>
        /// <param name="take">How many mailing email we can take</param>
        /// <param name="skip">How many mailing email we need to skip</param>
        /// <param name="mailingGroupId">Mailing group id related to mailing emails</param>
        /// <returns>Paginated mailing emails list</returns>
        [HttpGet("GetMailingEmails")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns mailing emails related to mailing group", typeof(IEnumerable<MailingEmailModel>))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized")]
        public async Task<IActionResult> GetMailingEmails([FromQuery] GetMailingEmailListRequestModel request)
        {
            var userId = HttpContext.GetUserId();
            return Ok(await _mediator.Send<IEnumerable<MailingEmailModel>>(new GetMailingEmailsQuery()
            {
                UserId = userId,
                MailingGroupId = request.MailingGroupId,
                Skip = request.Skip,
                Take = request.Take
            }));
        }

        /// <summary>
        /// Method used to create mailing email
        /// </summary>
        /// <param></param>
        /// <returns>Created mailing email id</returns>
        [HttpPost]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request", typeof(ApiErrorResultModel))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized")]
        public async Task<IActionResult> Create([FromBody] MailingEmailRequestModel request)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                return Ok(await _mediator.Send(new CreateMailingEmailCommand()
                {
                    UserId = userId,
                    MailingGroupId = request.MailingGroupId,
                    Email = request.Email
                }));
            }
            catch (Exception ex)
            {
                return ProcessErrorResponse(ex);
            }
        }

        /// <summary>
        /// Method used to update mailing email
        /// </summary>
        /// <param name="id">Mailing email id</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.NoContent, type: typeof(void))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request", typeof(ApiErrorResultModel))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MailingEmailRequestModel request)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                await _mediator.Send(new UpdateMailingEmailCommand()
                {
                    UserId = userId,
                    NewEmailName = request.Email,
                    MailingGroupId = request.MailingGroupId,
                    MailingEmailId = id
                });

                return NoContent();
            }
            catch (Exception ex)
            {
                return ProcessErrorResponse(ex);
            }
        }

        /// <summary>
        /// Method used delete mailing email
        /// </summary>
        /// <param name="id">Mailing email id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.NoContent, type: typeof(void))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request", typeof(ApiErrorResultModel))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                await _mediator.Send(new DeleteMailingEmailCommand()
                {
                    UserId = userId,
                    MailingEmailId = id,
                });

                return NoContent();
            }
            catch (Exception ex)
            {
                return ProcessErrorResponse(ex);
            }
        }
    }
}
