using MailingList.Api.Controllers.Base;
using MailingList.Api.Infrastructure.Extensions;
using MailingList.Api.Models;
using MailingList.Api.Models.Requests.Base;
using MailingList.Api.Models.Requests.MailingGroup;
using MailingList.Logic.Commands.MailingGroup;
using MailingList.Logic.Models.Responses;
using MailingList.Logic.Queries.MailingGroup;
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
    public class MailingGroupController : BaseApiController<MailingGroupController>
    {
        private readonly IMediator _mediator;

        public MailingGroupController(ILogger<MailingGroupController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Method used to get mailing groups for user
        /// </summary>
        /// <param name="take">How many mailing email we can take</param>
        /// <param name="skip">How many mailing email we need to skip</param>
        /// <returns>Paginated mailing groups list</returns>
        [HttpGet("GetMailingGroups")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns list group", typeof(IEnumerable<MailingGroupModel>))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized")]
        public async Task<IActionResult> GetMailingGroups([FromQuery] BasePagingListRequest request)
        {
            var userId = HttpContext.GetUserId();
            return Ok(await _mediator.Send<IEnumerable<MailingGroupModel>>(new GetMailingGroupsQuery()
            {
                UserId = userId,
                Skip = request.Skip,
                Take = request.Take
            }));
        }

        /// <summary>
        /// Method used to create mailing group
        /// </summary>
        /// <param></param>
        /// <returns>Created mailing group id</returns>
        [HttpPost]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request", typeof(ApiErrorResultModel))]
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

        /// <summary>
        /// Method used to update mailing group
        /// </summary>
        /// <param name="id">Mailing group id</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(void))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request", typeof(ApiErrorResultModel))]
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

        /// <summary>
        /// Method used delete mailing group
        /// </summary>
        /// <param name="id">Mailing group id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(void))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request", typeof(ApiErrorResultModel))]
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
