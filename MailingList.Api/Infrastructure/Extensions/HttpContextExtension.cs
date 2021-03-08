using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace MailingList.Api.Infrastructure.Extensions
{
    public static class HttpContextExtension
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            return Guid.Parse(httpContext.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value);
        }
    }
}
