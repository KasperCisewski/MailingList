using MailingList.Data.Domains.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace MailingList.Data.Domains
{
    public class MailingEmailGroup : IEntity<Guid>
    {
        public Guid Id { get; set; }

        [Required]
        public Guid MailingEmailId { get; set; }

        public virtual MailingEmail MailingEmail { get; set; }
        [Required]
        public Guid MailingGroupId { get; set; }

        public virtual MailingGroup MailingGroup { get; set; }
    }
}
