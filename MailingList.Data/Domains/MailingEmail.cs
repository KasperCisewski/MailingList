using MailingList.Data.Domains.Base;
using System;
using System.Collections.Generic;

namespace MailingList.Data.Domains
{
    public class MailingEmail : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public List<MailingEmailGroup> MailingEmailGroups { get; set; }
    }
}
