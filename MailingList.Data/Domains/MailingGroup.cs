using MailingList.Data.Domains.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MailingList.Data.Domains
{
    public class MailingGroup : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public List<MailingEmailGroup> MailingEmailGroups { get; set; }
    }
}
