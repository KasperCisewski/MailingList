using MailingList.Api.Controllers.Base;
using MailingList.Api.Infrastructure.Extensions;
using MailingList.Api.Models.Requests.MailingGroupEmail;
using MailingList.Logic.Commands.MailingEmail;
using MailingList.Logic.Queries.MailingEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MailingList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
    public class MailingEmailController : BaseApiController<MailingEmailController>
    {
        private readonly IMediator _mediator;

        public MailingEmailController(ILogger<MailingEmailController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpGet("GetMailingEmails")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns mailing emails related to mailing group")]
        //TODO
          //  [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(MailingEmailListExample))]
        public async Task<IActionResult> GetMailingEmails([FromQuery] GetMailingEmailListRequestModel request)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                return Ok(await _mediator.Send(new GetMailingEmailsQuery()
                {
                    UserId = userId,
                    MailingGroupId = request.MailingGroupId,
                    Skip = request.Skip,
                    Take = request.Take
                }));
            }
            catch (Exception ex)
            {
                return ProcessErrorResponse(ex);
            }
        }

        [HttpPost]
        [Authorize]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(Guid))]
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

        [HttpPut("{id}")]
        [Authorize]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(void))]
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

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(void))]
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
