using System;

namespace MailingList.Data.Domains
{
    public class MailingGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
    }
}
