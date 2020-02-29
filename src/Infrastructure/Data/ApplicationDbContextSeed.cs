using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CoreTemplate.ApplicationCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

// ReSharper disable all

namespace CoreTemplate.Infrastructure.Data
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser() {Id = Guid.Parse("0F97A74D-E52D-49B3-C241-08D7BD3C7B17"), UserName = "ddzbanowski", Email = "ddzbanowski@ddzbanowski" };

            if (userManager.Users.All(x => x.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, "passW0rd!");
            }
         }
    }
}
