using MailingList.Api.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace MailingList.Tests.Logic.Unit.Extensions
{
    public class HttpContextExtensionTests
    {
        private readonly Mock<HttpContext> _httpContext;

        public HttpContextExtensionTests()
        {
            _httpContext = new Mock<HttpContext>();
        }

        [Fact]
        public void GetUserIdForHttpContext_HttpContextUserHaveClaims_ShouldReturnId()
        {
            var id = Guid.NewGuid();
            _httpContext.Setup(x => x.User).Returns(new ClaimsPrincipal(new List<ClaimsIdentity>
            {
                new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, id.ToString())
                })
            }));

            var userId = _httpContext.Object.GetUserId();

            Assert.Equal(userId, id);
        }

        [Fact]
        public void GetUserIdForHttpContext_HttpContextUsedDoesnotHasClaims_ShouldThrowInvalidOperationException()
        {
            _httpContext.Setup(x => x.User).Returns(new ClaimsPrincipal(new List<ClaimsIdentity>
            {
            }));

            Assert.Throws<InvalidOperationException>(() => _httpContext.Object.GetUserId());
        }
    }
}
