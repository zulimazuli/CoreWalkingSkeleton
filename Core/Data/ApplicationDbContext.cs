﻿using System;
using CoreTemplate.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CoreTemplate.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Item> Items { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Person>()
                .HasKey(p => p.Id);

            builder.Entity<Person>()
                .HasOne(p => p.ApplicationUser)
                .WithOne(u => u.Person)
                .HasForeignKey<ApplicationUser>(u => u.PersonId);

            builder.Entity<Item>()
                .HasAlternateKey(x=>x.UniqueId);


            base.OnModelCreating(builder);
        }
    }
}
