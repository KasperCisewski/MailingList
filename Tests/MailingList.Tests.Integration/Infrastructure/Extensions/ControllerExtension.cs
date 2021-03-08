using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace MailingList.Tests.Integration.Infrastructure.Extensions
{
    internal static class ControllerExtension
    {
        public static void SetFakeHttpContextAndSetUser(this ControllerBase controller, Guid userId)
        {
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext(),
            };
            var userIdentity = new ClaimsIdentity();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userId.ToString())
            };

            userIdentity.AddClaims(claims);
            var claimPrincipal = new ClaimsPrincipal(userIdentity);
            controller.HttpContext.User = claimPrincipal;
        }
    }
}
