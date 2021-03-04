using MailingList.Logic.Data;
using MailingList.Logic.Validators;
using Xunit;

namespace MailingList.Tests.Logic.Unit.Validators
{
    public class IdentityValidatorTests
    {
        private readonly IdentityValidator _identityValidator;
        public IdentityValidatorTests()
        {
            _identityValidator = new IdentityValidator();
        }

        [Fact]
        public void ValidatePassword_PasswordIsLessThanEightChars_ShouldThrowLogicException()
        {
            var tooShortPassword = "Tes!t1";

            var exception = Assert.Throws<LogicException>(() => _identityValidator.ValidatePassword(tooShortPassword));

            Assert.Equal(LogicErrorCode.PasswordIsTooShort, exception.ErrorCode);
        }

        [Fact]
        public void ValidatePassword_PasswordDoesntHaveAnyUpperChar_ShouldThrowLogicException()
        {
            var passwordWithoutUpperChar = "test1234#as";

            var exception = Assert.Throws<LogicException>(() => _identityValidator.ValidatePassword(passwordWithoutUpperChar));

            Assert.Equal(LogicErrorCode.PasswordDoesntHaveUpperLetter, exception.ErrorCode);
        }

        [Fact]
        public void ValidatePassword_PasswordDoesntHaveAnyLowerChar_ShouldThrowLogicException()
        {
            var passwordWithoutlowerChar = "TEST1234#DSGF";

            var exception = Assert.Throws<LogicException>(() => _identityValidator.ValidatePassword(passwordWithoutlowerChar));

            Assert.Equal(LogicErrorCode.PasswordDoesntHaveLowerChar, exception.ErrorCode);
        }

        [Fact]
        public void ValidatePassword_PasswordDoesntHaveAnyNumber_ShouldThrowLogicException()
        {
            var passwordWithoutNumber = "TESTasda!#fsdAS";

            var exception = Assert.Throws<LogicException>(() => _identityValidator.ValidatePassword(passwordWithoutNumber));

            Assert.Equal(LogicErrorCode.PasswordDoesntHaveNumber, exception.ErrorCode);
        }

        [Fact]
        public void ValidatePassword_PasswordDoesntHaveSpecialChar_ShouldThrowLogicException()
        {
            var passwordWithoutSpecialChar = "TESTasda123sdfsdf1";
            var exception = Assert.Throws<LogicException>(() => _identityValidator.ValidatePassword(passwordWithoutSpecialChar));

            Assert.Equal(LogicErrorCode.PasswordDoesntHaveSpecialChar, exception.ErrorCode);
        }

        [Fact]
        public void ValidatePassword_PasswordHasMoreThanFiftyChars_ShouldThrowLogicException()
        {
            var validatedPasswordWithMoreThanFiftyChars = "ADsad123fdsg34345fgdSDF!#@23sadewsadasGHGDFMNGasdf4123asfdsf123432534dsfgfhgaSDSDGDFGas12345h";
            var exception = Assert.Throws<LogicException>(() => _identityValidator.ValidatePassword(validatedPasswordWithMoreThanFiftyChars));

            Assert.Equal(LogicErrorCode.PasswordIsTooLong, exception.ErrorCode);
        }

        [Fact]
        public void ValidatePassword_PasswordHasEightCharsAndHasUUpperLowerSpecialCharAndNumber_ShouldBeValidated()
        {
            var validatedPasswordWithEightChars = "Test123!";
            _identityValidator.ValidatePassword(validatedPasswordWithEightChars);
        }

        [Fact]
        public void ValidatePassword_PasswordHasFiftyCharsAndHasUUpperLowerSpecialCharAndNumber_ShouldBeValidated()
        {
            var validatedPasswordWithFiftyChars = "ADsad123fdsg34345fgdSDF!#@23sadewsadasGHGDFMNGasdf";
            _identityValidator.ValidatePassword(validatedPasswordWithFiftyChars);
        }

        [Fact]
        public void ValidatePassword_PasswordHasMoreThanEightAndLessThanFiftyCharsAndHasUpperLowerSpecialCharAndNumber_ShouldBeValidated()
        {
            var validatedPasswordWithEightChars = "Test123$Test123@#";
            _identityValidator.ValidatePassword(validatedPasswordWithEightChars);
        }
    }
}
