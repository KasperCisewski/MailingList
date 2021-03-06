using MailingList.Api.Controllers.Base;
using MailingList.Api.Infrastructure.Extensions;
using MailingList.Api.Models.Requests.Base;
using MailingList.Api.Models.Requests.MailingGroup;
using MailingList.Logic.Commands.MailingGroup;
using MailingList.Logic.Queries.MailingGroup;
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
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized")]
    public class MailingGroupController : BaseApiController<MailingGroupController>
    {
        private readonly IMediator _mediator;

        public MailingGroupController(ILogger<MailingGroupController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpGet("GetGroups")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns list group")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized")]
        //    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(MailingGroupListExample))]
        public async Task<IActionResult> GetMailingGroups([FromQuery] BasePagingListRequest request)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                return Ok(await _mediator.Send(new GetMailingGroupsQuery()
                {
                    UserId = userId,
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
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized")]
        public async Task<IActionResult> Create([FromBody] MailingGroupRequestModel request)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                return Ok(await _mediator.Send(new CreateMailingGroupCommand()
                {
                    UserId = userId,
                    Name = request.Name
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
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MailingGroupRequestModel request)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                return Ok(await _mediator.Send(new UpdateMailingGroupCommand()
                {
                    UserId = userId,
                    MailingGroupId = id,
                    NewName = request.Name
                }));
            }
            catch (Exception ex)
            {
                return ProcessErrorResponse(ex);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(void))]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                return Ok(await _mediator.Send(new DeleteMailingGroupCommand()
                {
                    UserId = userId,
                    MailingGroupId = id,
                }));
            }
            catch (Exception ex)
            {
                return ProcessErrorResponse(ex);
            }
        }
    }
}
