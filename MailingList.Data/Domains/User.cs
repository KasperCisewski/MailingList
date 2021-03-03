using MailingList.Data.Domains.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MailingList.Data.Domains
{
    public class User : IdentityUser<Guid>, IEntity<Guid>
    {
        public List<MailingGroup> MailingGroups { get; set; }
    }
}
