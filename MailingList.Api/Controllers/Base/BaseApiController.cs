using MailingList.Api.Models;
using MailingList.Logic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MailingList.Api.Controllers.Base
{
    public abstract class BaseApiController<T> : ControllerBase
    {
        protected readonly ILogger<T> Logger;

        public BaseApiController(ILogger<T> logger)
        {
            Logger = logger;
        }

        protected IActionResult ProcessErrorResponse(Exception ex)
        {
            Logger.LogError(ex.ToString());

            return BadRequest(new ApiErrorResultModel()
            {
                LogicErrorCode = ex is LogicException ? (ex as LogicException).ErrorCode : LogicErrorCode.DefaultError,
                ErrorMessage = ex.Message
            });
        }
    }
}
