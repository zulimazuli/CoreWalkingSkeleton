using System;
using Microsoft.AspNetCore.Identity;

namespace CoreTemplate.Models
{
    public sealed class ApplicationUser : IdentityUser<Guid>
    {
        public string CustomTag { get; set; }

        public int? PersonId { get; set; }
        public Person Person { get; set; }
    }
}