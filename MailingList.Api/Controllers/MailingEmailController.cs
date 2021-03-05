using MailingList.Api.Models.Requests.MailingGroupEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MailingList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerResponse(HttpStatusCode.BadRequest, "Bad Request")]
    public class MailingEmailController : ControllerBase
    {
        private readonly ILogger<MailingGroupController> _logger;
        private readonly IMediator _mediator;

        public MailingEmailController(ILogger<MailingGroupController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("GetMailingEmails")]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, "Returns mailing emails related to mailing group")]
        //  [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(SuccessfullLoginAndRegistrationExample))]
        public async Task<IActionResult> GetEmails(GetMailingEmailListRequestModel model)
        {
            var user = HttpContext.User.Claims.ToList(); ;

            throw new NotImplementedException();
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] MailingEmailRequestModel mailingGroupModel)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] MailingEmailRequestModel mailingGroupModel)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
