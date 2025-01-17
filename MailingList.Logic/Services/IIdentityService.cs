﻿using MailingList.Data.Domains;
using MailingList.Logic.Models.Responses;

namespace MailingList.Logic.Services
{
    internal interface IIdentityService : IService
    {
        bool UserWithEmailExists(string email);
        bool UserWithUsernameExists(string username);
        AuthorizationSuccessResponse GenerateAuthorizationResultForUser(User user, string secret);
    }
}
