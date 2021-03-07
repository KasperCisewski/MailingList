using MailingList.Logic.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace MailingList.Logic.Validators
{
    public class IdentityValidator
    {
        private const int _maxPasswordLength = 50;

        public void ValidatePassword(string password)
        {
            if (password.Length < 8)
                throw new LogicException(LogicErrorCode.PasswordIsTooShort, "Password is too short!");

            if (!password.Any(c => char.IsUpper(c)))
                throw new LogicException(LogicErrorCode.PasswordDoesntHaveUpperLetter, "Password should have upper letter!");

            if (!password.Any(c => char.IsLower(c)))
                throw new LogicException(LogicErrorCode.PasswordDoesntHaveLowerChar, "Password should have lower case letter!");

            if (!password.Any(c => char.IsDigit(c)))
                throw new LogicException(LogicErrorCode.PasswordDoesntHaveNumber, "Password should have number!");

            var regexToCheckIfPasswordContainSpecialChar = new Regex("[^A-Za-z0-9]");

            if (!regexToCheckIfPasswordContainSpecialChar.IsMatch(password))
                throw new LogicException(LogicErrorCode.PasswordDoesntHaveSpecialChar, "Password should have special character!");

            if (password.Length > _maxPasswordLength)
                throw new LogicException(LogicErrorCode.PasswordIsTooLong, $"Password should have less characters than {_maxPasswordLength}!");
        }

        public void ValidateEmail(string email)
        {
            if (!email.Contains('@'))
                throw new LogicException(LogicErrorCode.EmailShouldHaveAtChar, "Email should have @ char!");
        }
    }
}
