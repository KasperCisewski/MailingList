namespace MailingList.Logic.Data
{
    public enum LogicErrorCode : byte
    {
        DefaultError = 0,
        UserNameDoesNotHaveValue,
        LoginDoesNotHaveValue,
        EmailDoesNotHaveValue,
        PasswordDoesNotHaveValue,
        UserWithSameEmailExist,
        UserWithSameUsernameExist,
        FailedOnUserCreation,
        CannotFindUser,
        NotValidatedCredentials,
        PasswordIsTooShort,
        PasswordIsTooLong,
        PasswordDoesntHaveUpperLetter,
        PasswordDoesntHaveSpecialChar,
        PasswordDoesntHaveNumber,
        PasswordDoesntHaveLowerChar,
        EmailShouldHaveAtChar,
        DisallowToMakeChangesInOtherUserMailingGroup,
        MailingGroupNameMustBeUnique,
        CouldNotFindMailingGroup,
        MailingEmailGrourExist,
        CannotFindMailingEmail,
        NewNameAndOldNameShouldBeDifferent,
        CannotFindMailingGroup
    }
}
