using System;
using CoreTemplate.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreTemplate.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PersonalInformation> PersonalInformation { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PersonalInformation>()
                .HasKey(p => p.Id);

            builder.Entity<PersonalInformation>()
                .HasOne(p => p.ApplicationUser)
                .WithOne(u => u.PersonalInfo)
                .HasForeignKey<ApplicationUser>(u => u.PersonalInformationId);



            base.OnModelCreating(builder);
        }
    }
}
