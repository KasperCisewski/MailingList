using MailingList.Logic.Data;
using MailingList.Logic.Validators;
using Xunit;

namespace MailingList.Tests.Logic.Unit.Validators
{
    public class IdentityEmailValidatorTests
    {
        private readonly IdentityValidator _identityValidator;
        public IdentityEmailValidatorTests()
        {
            _identityValidator = new IdentityValidator();
        }

        [Fact]
        public void ValideteEmail_EmailHasAt_ShouldBeValidated()
        {
            var validatedEmail = "testEmail@test.com";

            _identityValidator.ValidateEmail(validatedEmail);
        }

        [Fact]
        public void ValideteEmail_EmailDoesntHaveAt_ShouldThrowLogicException()
        {
            var emailWithoutAtChar = "testEmail.com";

            var exception = Assert.Throws<LogicException>(() => _identityValidator.ValidateEmail(emailWithoutAtChar));

            Assert.Equal(LogicErrorCode.EmailShouldHaveAtChar, exception.ErrorCode);
        }
    }
}
