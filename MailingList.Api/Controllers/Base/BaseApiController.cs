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

        protected BaseApiController(ILogger<T> logger)
        {
            Logger = logger;
        }

        protected IActionResult ProcessErrorResponse(Exception ex)
        {
            Logger.LogError(ex.ToString());

            return BadRequest(new ApiErrorResultModel()
            {
                LogicErrorCode = ex is LogicException ? (ex as LogicException).ErrorCode : LogicErrorCode.DefaultError,
                LogicErrorCodeDescription = ex is LogicException ? (ex as LogicException).ErrorCode.ToString() : LogicErrorCode.DefaultError.ToString(),
                ErrorMessage = ex.Message
            });
        }
    }
}
