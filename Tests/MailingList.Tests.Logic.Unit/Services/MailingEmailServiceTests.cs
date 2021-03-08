using MailingList.Data.Domains;
using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Data;
using MailingList.Logic.Services.Implementation;
using MailingList.Logic.Validators;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MailingList.Tests.Logic.Unit.Services
{
    public class MailingEmailServiceTests
    {
        [Fact]
        public async Task GetOrCreateMailingEmail_EmailWasValidatedAndFound_EmailShouldBeFound()
        {
            var id = Guid.NewGuid();
            var mailingEmails = new List<MailingEmail>()
            {
                new MailingEmail
                {
                    Id = id,
                    Email = "test123@gmail.com"
                },
                new MailingEmail
                {
                    Id = Guid.NewGuid(),
                    Email = "test1234@gmail.com"
                }
            };

            var mailingEmailRepositoryMock = new Mock<IMailingEmailRepository>();

            mailingEmailRepositoryMock.Setup(m => m.GetAll()).Returns(mailingEmails.AsQueryable);

            var mailingEmailService = new MailingEmailService(mailingEmailRepositoryMock.Object, new IdentityValidator());

            var mailingEmailId = await mailingEmailService.GetOrCreateMailingEmail("test123@gmail.com");

            Assert.Equal(id, mailingEmailId);
        }

        [Fact]
        public async Task GetOrCreateMailingEmail_EmailWasNotValidatedAndNotFound_ShouldThrowException()
        {
            var mailingEmails = new List<MailingEmail>()
            {
                new MailingEmail
                {
                    Id = Guid.NewGuid(),
                    Email = "test123@gmail.com"
                },
                new MailingEmail
                {
                    Id = Guid.NewGuid(),
                    Email = "test1234@gmail.com"
                }
            };

            var mailingEmailRepositoryMock = new Mock<IMailingEmailRepository>();

            mailingEmailRepositoryMock.Setup(m => m.GetAll()).Returns(mailingEmails.AsQueryable);

            var mailingEmailService = new MailingEmailService(mailingEmailRepositoryMock.Object, new IdentityValidator());

            var exception = await Assert.ThrowsAsync<LogicException>
                (async () => await mailingEmailService.GetOrCreateMailingEmail("test1234"));

            Assert.Equal(LogicErrorCode.EmailShouldHaveAtChar, exception.ErrorCode);
        }

        [Fact]
        public async Task GetOrCreateMailingEmail_EmailWasValidatedAndNotFound_ShouldThrowException()
        {
            var mailingEmails = new List<MailingEmail>()
            {
                new MailingEmail
                {
                    Id = Guid.NewGuid(),
                    Email = "test123@gmail.com"
                },
                new MailingEmail
                {
                    Id= Guid.NewGuid(),
                    Email = "test1234@gmail.com"
                }
            };

            var mailingEmailRepositoryMock = new Mock<IMailingEmailRepository>();

            mailingEmailRepositoryMock.Setup(m => m.GetAll()).Returns(mailingEmails.AsQueryable);

            var mailingEmailService = new MailingEmailService(mailingEmailRepositoryMock.Object, new IdentityValidator());

            var mailingEmailId = await mailingEmailService.GetOrCreateMailingEmail("test12345@gmail.com");

            Assert.All(mailingEmails, e => Assert.NotEqual(e.Id, mailingEmailId));
        }
    }
}
