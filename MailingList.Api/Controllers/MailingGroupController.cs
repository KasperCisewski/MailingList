using MailingList.Api.Examples.Identity;
using MailingList.Api.Infrastructure.Extensions;
using MailingList.Api.Models.Requests.Base;
using MailingList.Api.Models.Requests.MailingGroup;
using MailingList.Logic.Commands.MailingGroup;
using MailingList.Logic.Data;
using MailingList.Logic.Queries.MailingGroup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MailingList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerResponse(HttpStatusCode.BadRequest, "Bad Request")]
    [SwaggerResponse(HttpStatusCode.Unauthorized, "Unauthorized")]
    public class MailingGroupController : ControllerBase
    {
        private readonly ILogger<MailingGroupController> _logger;
        private readonly IMediator _mediator;

        public MailingGroupController(ILogger<MailingGroupController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("GetGroups")]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, "Returns list group")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(SuccessfullLoginAndRegistrationExample))]
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

        [HttpPost("")]
        [Authorize]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(Guid))]
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

        [HttpPut("{id}")]
        [Authorize]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(void))]
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

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(void))]
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
