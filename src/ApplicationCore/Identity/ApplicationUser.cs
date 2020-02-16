using System;
using CoreTemplate.ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;

namespace CoreTemplate.ApplicationCore.Identity
{
    public sealed class ApplicationUser : IdentityUser<Guid>
    {
        public string CustomTag { get; set; }

        public int? PersonId { get; set; }
        public Person Person { get; set; }
    }
}