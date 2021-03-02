using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MailingList.Data.Domains
{
    public class User : IdentityUser<Guid>
    {
        public List<MailingGroup> MailingGroups { get; set; }
    }
}
