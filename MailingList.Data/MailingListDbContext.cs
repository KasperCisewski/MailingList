using MailingList.Data.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace MailingList.Data
{
    public class MailingListDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<MailingGroup> MailingGroups { get; set; }
        public DbSet<MailingEmail> MailingEmails { get; set; }

        public MailingListDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
