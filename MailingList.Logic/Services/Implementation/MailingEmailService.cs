using MailingList.Data.Domains;
using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Validators;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MailingList.Logic.Services.Implementation
{
    internal class MailingEmailService : IMailingEmailService
    {
        private readonly IMailingEmailRepository _mailingEmailRepository;
        private readonly IdentityValidator _identityValidator;

        public MailingEmailService(IMailingEmailRepository mailingEmailRepository, IdentityValidator identityValidator)
        {
            _mailingEmailRepository = mailingEmailRepository;
            _identityValidator = identityValidator;
        }

        public async Task<Guid> GetOrCreateMailingEmail(string email)
        {
            var mailingEmail = _mailingEmailRepository.GetAll()
                .FirstOrDefault(me => me.Email.ToLower() == email.ToLower());

            if (mailingEmail != null)
                return mailingEmail.Id;

            _identityValidator.ValidateEmail(email);

            mailingEmail = new MailingEmail()
            {
                Email = email
            };

            await _mailingEmailRepository.Add(mailingEmail);

            return mailingEmail.Id;
        }
    }
}
