using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CoreTemplate.ApplicationCore.Entities;
using CoreTemplate.ApplicationCore.Identity;
using CoreTemplate.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreTemplate.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Item> Items { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entityEntry in ChangeTracker.Entries<AuditableEntity>())
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entityEntry.Entity.Created = DateTime.Now;
                        entityEntry.Entity.CreatedBy =
                            _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                        break;
                    case EntityState.Modified:
                        entityEntry.Entity.LastModified = DateTime.Now;
                        entityEntry.Entity.LastModifiedBy =
                            _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                        break;
                }

            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Person>()
                .HasKey(p => p.Id);

            builder.Entity<Person>()
                .HasOne(p => p.ApplicationUser)
                .WithOne(u => u.Person)
                .HasForeignKey<ApplicationUser>(u => u.PersonId);

            builder.Entity<Item>()
                .HasAlternateKey(x => x.UniqueId);


            base.OnModelCreating(builder);
        }
    }
}