using MailingList.Data.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace MailingList.Data
{
    public class MailingListDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<MailingGroup> MailingGroup { get; set; }
        public DbSet<MailingEmail> MailingEmail { get; set; }
        public DbSet<MailingEmailGroup> MailingEmailGroup { get; set; }

        public MailingListDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("User");
            builder.Entity<IdentityRole<Guid>>().ToTable("Role");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRole");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaim");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaim");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken");

            builder.Entity<MailingGroup>().HasIndex(mailingGroup => mailingGroup.Name).IsUnique();
        }
    }
}
